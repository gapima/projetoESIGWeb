<%@ Control Language="C#" AutoEventWireup="true"
    CodeBehind="VincularVencimentosModal.ascx.cs"
    Inherits="ESIGWeb.Controls.VincularVencimentosModal" %>
<%@ Register TagPrefix="uc" TagName="NovoVencimentoModal"
    Src="~/Controls/NovoVencimentoModal.ascx" %>

<!-- Modal Principal: Vincular Crédito/Débito ao Vencimento -->
<div class="modal fade" id="vincularVencModal" clientIdMode="Static" tabindex="-1"
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
      <asp:UpdatePanel ID="updVincular" runat="server"
                       UpdateMode="Conditional">
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
                <asp:Button ID="btnNovoVencimento" runat="server" Text="+ Novo Vencimento"
                    OnClientClick="abrirModalNovoVencimento(); return false;" CssClass="btn btn-outline-secondary mb-2" />
            </div>

            <!-- Campos de edição do vencimento selecionado -->
            <div class="row mb-4">
              <div class="col-md-4">
                <label for="txtValor">Valor</label>
                <asp:TextBox ID="txtValor"
                             runat="server"
                             CssClass="form-control" />
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
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnNovoVencimento" EventName="Click" />
        </Triggers>
      </asp:UpdatePanel>

      <!-- Rodapé -->
      <div class="modal-footer">
        <asp:Button ID="btnExcluirVinc"
                    runat="server"
                    CssClass="btn btn-danger"
                    Text="Excluir vínculo"
                    OnClick="btnExcluirVinc_Click" />
        <asp:Button ID="btnSalvarVinc"
                    runat="server"
                    CssClass="btn btn-success"
                    Text="Salvar vínculo"
                    OnClick="btnSalvarVinc_Click" />
        <button type="button"
                class="btn btn-secondary"
                data-bs-dismiss="modal">
          Fechar
        </button>
      </div>
        <!-- modal filha fora do UpdatePanel -->
    <uc:NovoVencimentoModal ID="NovoVencimentoModal1" runat="server" />

    </div>
  </div>
</div>
