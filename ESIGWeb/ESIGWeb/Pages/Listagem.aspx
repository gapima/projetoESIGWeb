<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Listagem.aspx.cs" Inherits="ESIGWeb.Listagem" Async="true" %>
<%@ Register TagPrefix="uc" TagName="RowModal" Src="~/Controls/RowModal.ascx" %>
<%@ Register TagPrefix="uc" TagName="VincularVencimentosModal" Src="~/Controls/VincularVencimentosModal.ascx" %>

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
        ID="btnVincularVencimentos"
        runat="server"
        CssClass="btn btn-warning mt-3"
        Text="Vincular Créditos/ Débitos"
        OnClick="btnVincularVencimentos_Click" />

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
    <uc:VincularVencimentosModal ID="VincularVencimentosModal2" runat="server" />
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

<%--<script type="text/javascript">
    function openNewVencimentoModal() {
        // 1) Esconde o modal pai
        var paiEl = document.getElementById('vincularVencModal');
        var paiModal = bootstrap.Modal.getInstance(paiEl);
        if (paiModal) paiModal.hide();

        // 2) Aguarda para garantir que o DOM tenha removido o backdrop e foco
        setTimeout(function () {
            // 3) Obtém o elemento do modal filho
            var filhoEl = document.getElementById('novoVencModal');
            if (!filhoEl) {
                console.error('Modal filho não encontrado');
                return;
            }
            // 4) Cria/obtém a instância sem fazer o foco automático
            var filhoModal = bootstrap.Modal.getOrCreateInstance(filhoEl, {
                backdrop: 'static', // opcional
                focus: false       // **ESSENCIAL** para desativar o focus‐trap automático
            });
            // 5) Exibe o modal filho
            filhoModal.show();

            // 6) manualmente coloca foco no campo Descrição
            var desc = filhoEl.querySelector('input[id$="txtDescNovo"]');
            if (desc) desc.focus();
        }, 200);
    }
</script>--%>










