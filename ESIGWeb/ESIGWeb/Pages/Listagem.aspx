<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Listagem.aspx.cs" Inherits="ESIGWeb.Listagem" Async="true" %>
<%@ Register TagPrefix="uc" TagName="RowModal" Src="~/Controls/RowModal.ascx" %>
<%@ Register TagPrefix="uc" TagName="VincularVencimentosModal" Src="~/Controls/VincularVencimentosModal.ascx" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Listagem de Pessoas e Salários</title>

  <link
    href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css"
    rel="stylesheet" />
  <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>

  <style>
    body {
      background: #f8fafc;
    }
    .page-title {
      font-size: 2.5rem;
      font-weight: 700;
      letter-spacing: -1px;
      text-align: center;
      margin-bottom: 2rem;
      margin-top: 1.5rem;
    }
    .filtros-wrap {
      max-width: 850px;
      margin: 0 auto 2.2rem auto;
      padding: 2rem 2.5rem 1.2rem 2.5rem;
      background: #fff;
      border-radius: 1.5rem;
      box-shadow: 0 3px 14px rgba(0,0,0,0.08);
      display: flex;
      flex-wrap: wrap;
      align-items: center;
      gap: 1rem;
      justify-content: center;
    }
    .filtros-wrap .form-control {
      min-width: 210px;
      font-size: 1.1rem;
      border-radius: 2rem;
    }
    .filtros-wrap .btn {
      font-size: 1.1rem;
      border-radius: 2rem;
      margin-left: .25rem;
    }
    .table-responsive {
      background: #fff;
      border-radius: 1.2rem;
      box-shadow: 0 3px 14px rgba(0,0,0,0.08);
      padding: 1.2rem 1rem 0.2rem 1rem;
      margin-bottom: 2rem;
      position: relative;
    }
    .table thead th {
      background: #f2f6fa;
      font-weight: 700;
      font-size: 1.09rem;
      border-top: none;
      border-bottom: 2px solid #eaeaea;
    }
    .table tbody tr td {
      vertical-align: middle;
      font-size: 1.07rem;
      padding: 0.63rem 0.75rem;
      border-bottom: 1px solid #f1f1f1;
    }
    .table {
      margin-bottom: 0;
    }
    /* Ícone de atualizar VERDE */
    .refresh-btn {
      box-shadow: 0 2px 6px rgba(0,0,0,0.07);
      border-radius: 50%;
      border: 2px solid #38b54a;
      padding: 6px 8px 5px 8px;
      background: #fff;
      color: #38b54a;
      position: absolute;
      right: 20px;
      top: 14px;
      z-index: 10;
      transition: background 0.15s, color 0.15s, border 0.15s;
    }
    .refresh-btn svg {
      color: #38b54a;
      fill: #38b54a;
      stroke: #38b54a;
      vertical-align: middle;
    }
    .refresh-btn:hover {
      background: #e7fbe7;
      color: #249d34;
      border-color: #249d34;
    }
    .action-buttons-row {
      margin-top: 0.7rem;
      margin-bottom: 1.5rem;
      display: flex;
      flex-wrap: wrap;
      justify-content: space-between;
      align-items: center;
      gap: 1rem;
    }
    .action-buttons-left {
      display: flex;
      align-items: center;
      gap: 0.7rem;
      flex-wrap: wrap;
    }
    .action-buttons-right {
      display: flex;
      align-items: center;
      justify-content: flex-end;
      gap: 0.7rem;
    }
    .nova-pessoa-btn {
      min-width: 140px;
    }
    @media (max-width: 900px) {
      .filtros-wrap {
        flex-direction: column;
        gap: 0.6rem;
        padding: 1rem;
      }
      .action-buttons-row {
        flex-direction: column;
        gap: 0.6rem;
        align-items: stretch;
      }
      .action-buttons-right, .action-buttons-left {
        width: 100%;
        justify-content: stretch;
      }
      .nova-pessoa-btn {
        width: 100%;
      }
    }
  </style>
</head>
<body>

  <form id="form1" runat="server" class="container mt-2">

    <div class="d-flex justify-content-end align-items-center mt-3 mb-2" style="gap: 1rem;">
        <asp:Label ID="lblUsuarioLogado" runat="server" CssClass="fw-bold text-primary" />
        <asp:Button 
            ID="btnLogout" 
            runat="server" 
            CssClass="btn btn-outline-danger"
            Text="Sair"
            OnClick="btnLogout_Click" />
    </div>
    <!-- Toast de mensagem global -->
    <div aria-live="polite" aria-atomic="true" style="position: relative; min-height: 60px;">
      <div id="globalToast" class="toast align-items-center text-bg-success border-0"
           role="alert" aria-live="assertive" aria-atomic="true"
           style="position: absolute; top: 10px; right: 10px; min-width: 250px; z-index: 9999; display:none;">
        <div class="d-flex">
          <div class="toast-body" id="globalToastMsg">Sucesso!</div>
          <button type="button" class="btn-close btn-close-white me-2 m-auto"
                  data-bs-dismiss="toast" aria-label="Close"></button>
        </div>
      </div>
    </div>

    <asp:ScriptManager runat="server" />

    <!-- Título centralizado -->
    <div class="page-title">Listagem de Pessoas e Salários</div>

    <!-- Filtros modernos centralizados -->
    <div class="filtros-wrap">
      <asp:TextBox ID="txtFiltroNome" runat="server" CssClass="form-control" placeholder="Filtrar por nome" />
      <asp:TextBox ID="txtFiltroCargo" runat="server" CssClass="form-control" placeholder="Filtrar por cargo" />
      <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" CssClass="btn btn-primary" OnClick="btnFiltrar_Click" />
      <asp:Button ID="btnLimparFiltro" runat="server" Text="Limpar" CssClass="btn btn-outline-secondary" OnClick="btnLimparFiltro_Click" />
    </div>

    <!-- Grid moderna com botão de atualização no canto superior direito -->
    <div class="table-responsive">
      <button type="button"
              class="btn refresh-btn"
              data-bs-toggle="tooltip"
              title="Atualizar salários"
              onclick="__doPostBack('<%= btnCalcular.UniqueID %>', '')">
        <svg xmlns="http://www.w3.org/2000/svg" width="19" height="19" fill="currentColor" class="bi bi-arrow-clockwise" viewBox="0 0 16 16">
          <path fill-rule="evenodd" d="M8 3a5 5 0 1 1-4.546 2.914.5.5 0 0 1 .908-.417A4 4 0 1 0 8 4V1.5a.5.5 0 0 1 1 0v3A.5.5 0 0 1 8.5 5H5.5a.5.5 0 0 1 0-1H8z"/>
        </svg>
      </button>
      <asp:GridView
        ID="gridPessoas"
        runat="server"
        AutoGenerateColumns="false"
        AllowPaging="true"
        PageSize="10"
        OnPageIndexChanging="gridPessoas_PageIndexChanging"
        OnRowDataBound="gridPessoas_RowDataBound"
        CssClass="table table-hover align-middle"
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
    </div>

    <!-- Botões de ação (agora invertidos) -->
    <div class="action-buttons-row">
      <div class="action-buttons-left">
        <asp:Button
            ID="btnVincularVencimentos"
            runat="server"
            CssClass="btn btn-warning"
            Text="Vincular Créditos/ Débitos"
            OnClick="btnVincularVencimentos_Click" />

        <asp:Button
            ID="btnAddPessoa"
            runat="server"
            Text="Nova Pessoa"
            CssClass="btn btn-primary nova-pessoa-btn"
            OnClick="btnAddPessoa_Click" />
      </div>
      <div class="action-buttons-right">
        <asp:Button 
          ID="btnGerarRelatorio" 
          runat="server" 
          Text="Gerar Relatório"
          CssClass="btn btn-success"
          OnClick="btnGerarRelatorio_Click" />
      </div>
    </div>
    <!-- Botão oculto só para postback -->
    <asp:Button
      ID="btnCalcular"
      runat="server"
      Text="Recalcular Salários"
      CssClass="d-none" 
      OnClick="btnCalcular_Click" />

    <uc:RowModal ID="RowModal1" runat="server" />
    <uc:VincularVencimentosModal ID="VincularVencimentosModal2" runat="server" />

  </form>
</body>
</html>

<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>

<!-- Ativa o Tooltip do Bootstrap -->
<script>
    document.addEventListener('DOMContentLoaded', function () {
        var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
        tooltipTriggerList.forEach(function (tooltipTriggerEl) {
            new bootstrap.Tooltip(tooltipTriggerEl)
        })
    });
</script>

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
    function showGlobalToast(msg, success = true) {
        var toast = document.getElementById("globalToast");
        var toastMsg = document.getElementById("globalToastMsg");
        if (!toast || !toastMsg) return;
        toastMsg.innerText = msg;
        toast.classList.remove("text-bg-success", "text-bg-danger");
        toast.classList.add(success ? "text-bg-success" : "text-bg-danger");
        toast.style.display = "block";
        var bsToast = bootstrap.Toast.getOrCreateInstance(toast, { delay: 3500 });
        bsToast.show();
        setTimeout(() => { toast.style.display = "none"; }, 3700);
    }
</script>
