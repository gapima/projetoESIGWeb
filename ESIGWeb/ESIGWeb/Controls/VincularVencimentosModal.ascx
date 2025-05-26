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

            <!-- Bloco de erro para edição -->
            <div id="erroEditVenc" class="alert alert-danger d-none" role="alert"></div>

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
        <!-- Bloco de erro para novo -->
        <div id="erroNovoVenc" class="alert alert-danger d-none" role="alert"></div>

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
<script type="text/javascript">
    function openNewVencimentoModal() {
        const modalPaiEl = document.getElementById('<%= vincularVencModal.ClientID %>');
        const modalFilhoEl = document.getElementById('<%= novoVencModal.ClientID %>');

        const modalPai = bootstrap.Modal.getOrCreateInstance(modalPaiEl);
        const modalFilho = bootstrap.Modal.getOrCreateInstance(modalFilhoEl);

        // Fecha a modal pai
        modalPai.hide();

        setTimeout(() => {
            document.querySelectorAll('.modal-backdrop').forEach(e => e.remove());
            document.body.classList.remove('modal-open');
            modalPaiEl.classList.remove('show');
            modalPaiEl.setAttribute('aria-hidden', 'true');
            modalPaiEl.setAttribute('style', 'display: none;');
            document.getElementById('<%= txtDescNovo.ClientID %>').value = '';
            document.getElementById('<%= txtValorNovo.ClientID %>').value = '';
            document.getElementById('<%= ddlFormaNovo.ClientID %>').selectedIndex = 0;
            document.getElementById('<%= ddlTipoNovo.ClientID %>').selectedIndex = 0;
            modalFilho.show();
        }, 300);
    }
</script>

<script type="text/javascript">
    document.addEventListener("DOMContentLoaded", function () {
        // Máscara decimal nos inputs
        document.querySelectorAll(".valor-decimal").forEach(function (input) {
            input.addEventListener("input", function (e) {
                let valor = input.value.replace(/\D/g, '');
                if (valor === '') {
                    input.value = '';
                    return;
                }
                let intValue = parseInt(valor);
                let decimalValue = (intValue / 100).toFixed(2);
                input.value = decimalValue.replace('.', ',');
            });
            input.addEventListener("focus", function () {
                setTimeout(() => input.select(), 1);
            });
        });

        // Validação do formulário de NOVO vencimento
        const btnSalvarNovo = document.getElementById('<%= btnSalvarNovo.ClientID %>');
        if (btnSalvarNovo) {
            btnSalvarNovo.onclick = function (e) {
                const desc = document.getElementById('<%= txtDescNovo.ClientID %>').value.trim();
                const valor = document.getElementById('<%= txtValorNovo.ClientID %>').value.replace(',', '.').trim();
                const forma = document.getElementById('<%= ddlFormaNovo.ClientID %>').value;
                const tipo = document.getElementById('<%= ddlTipoNovo.ClientID %>').value;
                let erros = [];

                if (!desc) erros.push("Descrição é obrigatória.");
                if (!valor || isNaN(valor) || Number(valor) < 0) erros.push("Valor deve ser um número positivo.");
                if (!forma) erros.push("Selecione a forma de incidência.");
                if (!tipo) erros.push("Selecione o tipo.");

                if (erros.length > 0) {
                    e.preventDefault();
                    const erroDiv = document.getElementById('erroNovoVenc');
                    erroDiv.innerHTML = erros.join("<br>");
                    erroDiv.classList.remove('d-none');
                    return false;
                } else {
                    document.getElementById('erroNovoVenc').classList.add('d-none');
                }
            }
        }

        // Validação do formulário de EDIÇÃO de vencimento
        const btnSalvarVinc = document.getElementById('<%= btnSalvarVinc.ClientID %>');
        if (btnSalvarVinc) {
            btnSalvarVinc.onclick = function (e) {
                const valor = document.getElementById('<%= txtValor.ClientID %>').value.replace(',', '.').trim();
                const forma = document.getElementById('<%= ddlForma.ClientID %>').value;
                const tipo = document.getElementById('<%= ddlTipo.ClientID %>').value;
                let erros = [];

                if (!valor || isNaN(valor) || Number(valor) < 0) erros.push("Valor deve ser um número positivo.");
                if (!forma) erros.push("Selecione a forma de incidência.");
                if (!tipo) erros.push("Selecione o tipo.");

                if (erros.length > 0) {
                    e.preventDefault();
                    const erroDiv = document.getElementById('erroEditVenc');
                    erroDiv.innerHTML = erros.join("<br>");
                    erroDiv.classList.remove('d-none');
                    return false;
                } else {
                    document.getElementById('erroEditVenc').classList.add('d-none');
                }
            }
        }
    });
</script>
