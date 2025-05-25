<%@ Control Language="C#" AutoEventWireup="true"
    CodeBehind="NovoVencimentoModal.ascx.cs"
    Inherits="ESIGWeb.Controls.NovoVencimentoModal" %>

<div id="novoVencModal" runat="server" class="modal fade" clientIdMode="Static" tabindex="-1" aria-labelledby="novoVencModalLabel" aria-hidden="true">
  <div class="modal-dialog">
    <div class="modal-content">
      <!-- header -->
      <div class="modal-header">
        <h5 class="modal-title" id="novoVencModalLabel">Novo Vencimento</h5>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Fechar"></button>
      </div>
      <!-- body -->
      <asp:UpdatePanel ID="updNovoVenc" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
          <div class="modal-body">
            <div class="mb-3">
              <label for="txtDescNovo" class="form-label">Descrição</label>
              <asp:TextBox ID="txtDescNovo" runat="server" CssClass="form-control" />
            </div>
            <div class="mb-3">
              <label for="txtValorNovo" class="form-label">Valor</label>
              <asp:TextBox ID="txtValorNovo" runat="server" CssClass="form-control" TextMode="Number" />
            </div>
            <div class="mb-3">
              <label for="ddlFormaNovo" class="form-label">Forma Incidência</label>
              <asp:DropDownList ID="ddlFormaNovo" runat="server" CssClass="form-select">
                <asp:ListItem Value="V">Valor</asp:ListItem>
                <asp:ListItem Value="P">%</asp:ListItem>
              </asp:DropDownList>
            </div>
            <div class="mb-3">
              <label for="ddlTipoNovo" class="form-label">Tipo</label>
              <asp:DropDownList ID="ddlTipoNovo" runat="server" CssClass="form-select">
                <asp:ListItem Value="C">Crédito</asp:ListItem>
                <asp:ListItem Value="D">Débito</asp:ListItem>
              </asp:DropDownList>
            </div>
          </div>
        </ContentTemplate>
      </asp:UpdatePanel>
      <!-- footer -->
      <div class="modal-footer">
        <asp:Button ID="btnSalvarNovo" runat="server" CssClass="btn btn-primary" Text="Salvar" OnClick="btnSalvarNovo_Click" />
        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
      </div>
    </div>
  </div>
</div>
