<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Listagem.aspx.cs" Inherits="ESIGWeb.Listagem" %>
<%@ Register TagPrefix="uc" TagName="RowModal" Src="~/Controls/RowModal.ascx" %>
<%@ Register TagPrefix="uc" TagName="CalcularSalarioModal" Src="~/Controls/CalcularSalarioModal.ascx" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Listagem de Pessoas e Salários</title>

  <!-- CSS do Bootstrap no head -->
  <link
    href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css"
    rel="stylesheet" />

  <!-- JS do Bootstrap no head, para garantir que 'bootstrap' existe antes de qualquer script -->
  <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>

  <style>
    .table tbody tr:hover td {
      background-color: #f0f8ff !important;
      cursor: pointer;
    }
    .table tbody tr.selected td {
      background-color: #ffe4b5 !important;
    }
  </style>
</head>
<body>
  <form id="form1" runat="server" class="container mt-4">
    <asp:ScriptManager runat="server" />

    <h2>Listagem de Pessoas e Salários</h2>

    <asp:GridView
      ID="gridPessoas"
      runat="server"
      AutoGenerateColumns="false"
      AllowPaging="true"
      PageSize="10"
      OnPageIndexChanging="gridPessoas_PageIndexChanging"
      OnRowDataBound="gridPessoas_RowDataBound"
      CssClass="table table-bordered"
      DataKeyNames="pessoa_id"
      EmptyDataText="Nenhum registro encontrado.">
      <Columns>
        <asp:BoundField DataField="pessoa_id" HeaderText="ID" />
        <asp:BoundField DataField="nome" HeaderText="Nome" />
        <asp:BoundField DataField="salario_bruto" HeaderText="Salário Bruto" DataFormatString="{0:C}" />
        <asp:BoundField DataField="descontos" HeaderText="Descontos" DataFormatString="{0:C}" />
        <asp:BoundField DataField="salario_liquido" HeaderText="Salário Líquido" DataFormatString="{0:C}" />
        <asp:BoundField DataField="nome_cargo" HeaderText="Cargo" />
      </Columns>
    </asp:GridView>

    <asp:Button
      ID="btnCalcularSalario"
      runat="server"
      Text="Calcular Salários"
      CssClass="btn btn-primary mt-3"
      OnClick="btnCalcularSalario_Click" />
    <asp:Button
      ID="btnCalcular"
      runat="server"
      Text="Recalcular Salários"
      CssClass="btn btn-primary mt-3"
      OnClick="btnCalcular_Click" />
    <asp:Button
        ID="btnAddPessoa"
        runat="server"
        Text="Nova Pessoa"
        CssClass="btn btn-primary mt-3"
        style="float: right"
        OnClick="btnAddPessoa_Click" />


    <uc:RowModal ID="RowModal1" runat="server" />
    <uc:CalcularSalarioModal ID="CalcularSalarioModal1" runat="server" />
  </form>
</body>
</html>

<!-- Scripts Bootstrap já existentes -->
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>

<!-- Script de validação de CEP -->
<script type="text/javascript">
    document.addEventListener('DOMContentLoaded', function () {
        // Captura o botão OK e os inputs do RowModal
        var btnOk = document.getElementById('<%= RowModal1.FindControl("btnValidateCep").ClientID %>');
        var txtCep = document.getElementById('<%= RowModal1.FindControl("txtCEP").ClientID %>');
        var txtEndereco = document.getElementById('<%= RowModal1.FindControl("txtEndereco").ClientID %>');
        var txtCidade = document.getElementById('<%= RowModal1.FindControl("txtCidade").ClientID %>');

      // Handler de clique
      btnOk.addEventListener('click', function (e) {
          e.preventDefault();  // evita qualquer postback
          var cep = txtCep.value.replace(/\D/g, '');
          if (cep.length !== 8) {
              alert('O CEP deve ter exatamente 8 dígitos.');
              return;
          }
          txtEndereco.value = 'Buscando…';
          txtCidade.value = '';
          fetch('https://brasilapi.com.br/api/cep/v1/' + cep)
              .then(function (resp) {
                  if (!resp.ok) throw resp;
                  return resp.json();
              })
              .then(function (data) {
                  txtCidade.value = data.city || '';
                  var parts = [];
                  if (data.street) parts.push(data.street);
                  if (data.neighborhood) parts.push(data.neighborhood);
                  if (data.state) parts.push(data.state);
                  txtEndereco.value = parts.join(', ');
              })
              .catch(function () {
                  alert('CEP inválido ou não encontrado.');
                  txtEndereco.value = '';
                  txtCidade.value = '';
              });
      });
  });
</script>

<script type="text/javascript">
    function openNewPessoaModal() {
        // Obtém referência à modal
        var modalEl = document.getElementById('rowModal');
        var modal = new bootstrap.Modal(modalEl);

        // Limpa todos os campos da aba Pessoa
      document.getElementById('<%= RowModal1.FindControl("txtPessoaId").ClientID %>').value = '0';
      document.getElementById('<%= RowModal1.FindControl("txtPessoaNome").ClientID %>').value = '';
      document.getElementById('<%= RowModal1.FindControl("txtDataNascimento").ClientID %>').value = '';
      document.getElementById('<%= RowModal1.FindControl("txtEmail").ClientID %>').value = '';
      document.getElementById('<%= RowModal1.FindControl("txtUsuario").ClientID %>').value = '';
      document.getElementById('<%= RowModal1.FindControl("txtCidade").ClientID %>').value = '';
      document.getElementById('<%= RowModal1.FindControl("txtCEP").ClientID %>').value = '';
      document.getElementById('<%= RowModal1.FindControl("txtEndereco").ClientID %>').value = '';
      document.getElementById('<%= RowModal1.FindControl("txtPais").ClientID %>').value = '';
      document.getElementById('<%= RowModal1.FindControl("txtTelefone").ClientID %>').value = '';

    // Zera o dropdown de cargos (seleciona o primeiro item “-- selecione --”)
    var ddl = document.getElementById('<%= RowModal1.FindControl("ddlCargo").ClientID %>');
    if (ddl) ddl.selectedIndex = 0;

    // Esconde mensagens de validação
    var valSum = document.querySelector('#<%= RowModal1.ClientID %> .alert');
        if (valSum) valSum.style.display = 'none';

        // Abre a modal
        modal.show();
    }
</script>


<!-- Script JS local para adicionar linhas -->
<script type="text/javascript">
    function addRow(tableId) {
        var table = document.getElementById(tableId).getElementsByTagName('tbody')[0];
        var row = table.insertRow();
        row.innerHTML = `
      <td><input type="text" class="form-control" /></td>
      <td><input type="number" class="form-control" min="0" step="0.01" /></td>
      <td>
        <select class="form-select">
          <option value="V">Valor</option>
          <option value="P">%</option>
        </select>
      </td>
      <td>
        <button type="button" class="btn btn-danger btn-sm" onclick="this.closest('tr').remove();">🗑️</button>
      </td>
    `;
    }
</script>

<!-- Bootstrap Icons -->
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.min.css">
<script type="text/javascript">
    function addRowSalario(tableId) {
        var table = document.getElementById(tableId).getElementsByTagName('tbody')[0];
        var row = table.insertRow();
        row.innerHTML = `
    <td>
      <input type="text" class="form-control form-control-sm" />
    </td>
    <td>
      <input type="number" class="form-control form-control-sm text-end" min="0" step="0.01" />
    </td>
    <td>
      <select class="form-select form-select-sm">
        <option value="V">V</option>
        <option value="P">%</option>
      </select>
    </td>
    <td>
      <button type="button" class="btn btn-danger btn-sm" title="Remover" onclick="this.closest('tr').remove();">
        <i class="bi bi-trash"></i>
      </button>
    </td>
  `;
    }
</script>