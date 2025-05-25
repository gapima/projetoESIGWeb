<%@ Control Language="C#" AutoEventWireup="true"
    CodeBehind="VincularVencimentosModal.ascx.cs"
    Inherits="ESIGWeb.Controls.VincularVencimentosModal" %>

<!-- Modal Principal: Vincular Crédito/Débito ao Vencimento -->
<div id="vincularVencModal" runat="server" clientIdMode="Static" class="modal fade" tabindex="-1"
     aria-labelledby="vincularVencModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-lg">
    <div class="modal-content">

      <!-- Cabeçalho -->
      <div class="modal-header">
        <h5 class="modal-title" id="vincularVencModalLabel">
          Vincular Crédito/Débito ao Vencimento
        </h5>
        <button type="button" class="btn-close"
                data-bs-dismiss="modal" aria-label="Fechar"></button>
      </div>

      <!-- Body com UpdatePanel -->
      <asp:UpdatePanel ID="updVincular" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
          <div class="modal-body">

            <!-- Linha: dropdown de vencimentos + botão de “Novo” -->
            <div class="d-flex align-items-center mb-3">
              <label class="me-2 mb-0">Vencimento:</label>
              <asp:DropDownList ID="ddlVencimentos"
                                runat="server"
                                CssClass="form-select me-3"
                                Width="300px"
                                AutoPostBack="true"
                                OnSelectedIndexChanged="ddlVencimentos_SelectedIndexChanged" />
              <button type="button"
                      class="btn btn-outline-secondary mb-2"
                    OnClick="openNewVencimentoModal();">
                + Novo Vencimento
              </button>
            </div>

            <!-- Campos de edição do vencimento selecionado -->
            <div class="row mb-4">
              <div class="col-md-4">
                <label for="txtValor">Valor</label>
                <asp:TextBox ID="txtValor"
                             runat="server"
                             CssClass="form-control valor-decimal" />
              </div>
              <div class="col-md-4">
                <label for="ddlForma">Forma Incidência</label>
                <asp:DropDownList ID="ddlForma"
                                  runat="server"
                                  CssClass="form-select">
                  <asp:ListItem Value="V">Valor</asp:ListItem>
                  <asp:ListItem Value="P">%</asp:ListItem>
                </asp:DropDownList>
              </div>
              <div class="col-md-4">
                <label for="ddlTipo">Tipo</label>
                <asp:DropDownList ID="ddlTipo"
                                  runat="server"
                                  CssClass="form-select">
                  <asp:ListItem Value="C">Crédito</asp:ListItem>
                  <asp:ListItem Value="D">Débito</asp:ListItem>
                </asp:DropDownList>
              </div>
            </div>

            <!-- Repeater de cargos com checkbox -->
            <h6>Cargos vinculados</h6>
            <asp:Repeater ID="rptCargos" runat="server">
              <ItemTemplate>
                <div class="form-check">
                  <input class="form-check-input"
                         type="checkbox"
                         id="chkCargo_<%# Eval("Id") %>"
                         name="chkCargo"
                         value='<%# Eval("Id") %>'
                         <%# (bool)Eval("Vinculado") ? "checked" : "" %> />
                  <label class="form-check-label"
                         for="chkCargo_<%# Eval("Id") %>">
                    <%# Eval("Nome") %>
                  </label>
                </div>
              </ItemTemplate>
            </asp:Repeater>

          </div>
        </ContentTemplate>
      </asp:UpdatePanel>

      <!-- Rodapé -->
      <div class="modal-footer">
        <asp:Button ID="btnExcluirVinc"
                    runat="server"
                    CssClass="btn btn-danger"
                    Text="Excluir"
                    OnClick="btnExcluirVinc_Click" />
        <asp:Button ID="btnSalvarVinc"
                    runat="server"
                    CssClass="btn btn-success"
                    Text="Salvar"
                    OnClick="btnSalvarVinc_Click" />
        <button type="button"
                class="btn btn-secondary"
                data-bs-dismiss="modal">
          Fechar
        </button>
      </div>
    </div>
  </div>
</div>

<!-- Modal Filho: Novo Vencimento -->
<div id="novoVencModal" runat="server" clientIdMode="Static" class="modal fade" tabindex="-1"
     aria-labelledby="novoVencModalLabel" aria-hidden="true">
  <div class="modal-dialog">
    <div class="modal-content">

      <!-- Header -->
      <div class="modal-header">
        <h5 class="modal-title" id="novoVencModalLabel">Novo Vencimento</h5>
        <button type="button" class="btn-close"
                data-bs-dismiss="modal" aria-label="Fechar"></button>
      </div>

      <!-- Body -->
      <div class="modal-body">
        <div class="mb-3">
          <label for="txtDescNovo" class="form-label">Descrição</label>
          <asp:TextBox ID="txtDescNovo" runat="server"
                       CssClass="form-control" />
        </div>
        <div class="mb-3">
          <label for="txtValorNovo" class="form-label">Valor</label>
          <asp:TextBox ID="txtValorNovo" runat="server"
                       CssClass="form-control valor-decimal" />
        </div>
        <div class="mb-3">
          <label for="ddlFormaNovo" class="form-label">Forma Incidência</label>
          <asp:DropDownList ID="ddlFormaNovo" runat="server"
                            CssClass="form-select">
            <asp:ListItem Value="V">Valor</asp:ListItem>
            <asp:ListItem Value="P">%</asp:ListItem>
          </asp:DropDownList>
        </div>
        <div class="mb-3">
          <label for="ddlTipoNovo" class="form-label">Tipo</label>
          <asp:DropDownList ID="ddlTipoNovo" runat="server"
                            CssClass="form-select">
            <asp:ListItem Value="C">Crédito</asp:ListItem>
            <asp:ListItem Value="D">Débito</asp:ListItem>
          </asp:DropDownList>
        </div>
      </div>

      <!-- Footer -->
      <div class="modal-footer">
        <asp:Button ID="btnSalvarNovo" runat="server"
                    CssClass="btn btn-primary"
                    Text="Salvar"
                    OnClick="btnSalvarNovo_Click" />
        <button type="button" class="btn btn-secondary"
                data-bs-dismiss="modal">
          Cancelar
        </button>
      </div>
    </div>
  </div>
</div>

<!-- Abre a modal “Novo Vencimento” -->
<%--<script type="text/javascript">
  function openNewVencimentoModal() {
    // esconde a modal pai (caso esteja aberta)
    var pai = bootstrap.Modal.getInstance(
      document.getElementById('<%= vincularVencModal.ClientID %>')
    );
    if (pai) pai.hide();

    // abre a modal filho
    var filho = new bootstrap.Modal(
      document.getElementById('<%= novoVencModal.ClientID %>')
    );
        filho.show();
    }
</script>--%>
<script type="text/javascript">
    function openNewVencimentoModal() {
        const modalPaiEl = document.getElementById('<%= vincularVencModal.ClientID %>');
    const modalFilhoEl = document.getElementById('<%= novoVencModal.ClientID %>');

        const modalPai = bootstrap.Modal.getOrCreateInstance(modalPaiEl);
        const modalFilho = bootstrap.Modal.getOrCreateInstance(modalFilhoEl);

        // Fecha a modal pai
        modalPai.hide();

        // Remoção completa dos vestígios da modal anterior
        setTimeout(() => {
            // Garante que backdrop sumiu
            document.querySelectorAll('.modal-backdrop').forEach(e => e.remove());

            // Remove classe que prende o scroll
            document.body.classList.remove('modal-open');

            // Remove classe "show" manualmente da modal pai
            modalPaiEl.classList.remove('show');
            modalPaiEl.setAttribute('aria-hidden', 'true');
            modalPaiEl.setAttribute('style', 'display: none;');


            document.getElementById('<%= txtDescNovo.ClientID %>').value = '';
            document.getElementById('<%= txtValorNovo.ClientID %>').value = '';
            document.getElementById('<%= ddlFormaNovo.ClientID %>').selectedIndex = 0;
            document.getElementById('<%= ddlTipoNovo.ClientID %>').selectedIndex = 0;
            // Agora abre a nova modal
            modalFilho.show();
        }, 300); // espera a transição da primeira modal
    }
</script>

<script type="text/javascript">
    document.addEventListener("DOMContentLoaded", function () {
        document.querySelectorAll(".valor-decimal").forEach(function (input) {
            input.addEventListener("input", function (e) {
                // Remove tudo que não é número
                let valor = input.value.replace(/\D/g, '');

                // Se estiver vazio, zera
                if (valor === '') {
                    input.value = '';
                    return;
                }

                // Converte para decimal em tempo real (2 casas)
                let intValue = parseInt(valor);
                let decimalValue = (intValue / 100).toFixed(2);

                // Formata com vírgula (pt-BR)
                input.value = decimalValue.replace('.', ',');
            });

            // (Opcional) Seleciona tudo ao focar, para facilitar digitação
            input.addEventListener("focus", function () {
                setTimeout(() => input.select(), 1);
            });
        });
    });
</script>

