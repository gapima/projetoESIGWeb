<%@ Control Language="C#" AutoEventWireup="true" 
    CodeBehind="CalcularSalarioModal.ascx.cs" 
    Inherits="ESIGWeb.Controls.CalcularSalarioModal" %>

<!-- Modal Calcular Salário -->
<div class="modal fade" id="calcularSalarioModal" tabindex="-1" 
     aria-labelledby="calcularSalarioModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-xl">
    <div class="modal-content">

      <div class="modal-header">
        <h5 class="modal-title" id="calcularSalarioModalLabel">
          Editar Créditos e Débitos do Cargo
        </h5>
        <button type="button" class="btn-close" 
                data-bs-dismiss="modal" aria-label="Fechar"></button>
      </div>

      <asp:UpdatePanel ID="updCalcularSalarioBody" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
          <div class="modal-body">
            <!-- Combobox de Cargo -->
            <div class="row mb-3">
              <label class="col-sm-2 col-form-label" for="ddlCargo">Cargo:</label>
              <div class="col-sm-4">
                <asp:DropDownList 
                  ID="ddlCargo" 
                  runat="server" 
                  CssClass="form-select"
                  AutoPostBack="true"
                  OnSelectedIndexChanged="ddlCargo_SelectedIndexChanged" />
              </div>
            </div>

            <!-- Créditos -->
            <h6>Créditos</h6>
            <asp:GridView
              ID="gridCreditos"
              runat="server"
              AutoGenerateColumns="false"
              CssClass="table table-sm table-bordered align-middle mb-2">
              <Columns>
                <asp:TemplateField HeaderText="Descrição">
                  <ItemTemplate>
                    <input type="text" 
                           name="cred_desc" 
                           class="form-control form-control-sm" 
                           value='<%# Eval("Descricao") %>' />
                  </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Valor">
                  <ItemTemplate>
                    <input type="number" 
                           name="cred_valor" 
                           class="form-control form-control-sm text-end" 
                           value='<%# Eval("Valor") %>' 
                           min="0" step="0.01" />
                  </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Incidência">
                  <ItemTemplate>
                    <select name="cred_incid" 
                            class="form-select form-select-sm">
                      <option value="V" 
                        <%# Eval("FormaIncidencia").ToString()=="V" ? "selected" : "" %>>
                        Valor
                      </option>
                      <option value="P" 
                        <%# Eval("FormaIncidencia").ToString()=="P" ? "selected" : "" %>>
                        %
                      </option>
                    </select>
                  </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Ações">
                  <ItemTemplate>
                    <button type="button" 
                            class="btn btn-danger btn-sm" 
                            onclick="if(confirm('Confirma exclusão?')) this.closest('tr').remove();">
                      <i class="bi bi-trash"></i>
                    </button>
                  </ItemTemplate>
                </asp:TemplateField>
              </Columns>
            </asp:GridView>
            <button type="button" 
                    class="btn btn-outline-primary btn-sm mb-3" 
                    onclick="addRowSalario('<%= gridCreditos.ClientID %>')">
              + Adicionar Crédito
            </button>

            <!-- Débitos -->
            <h6>Débitos</h6>
            <asp:GridView
              ID="gridDebitos"
              runat="server"
              AutoGenerateColumns="false"
              CssClass="table table-sm table-bordered align-middle mb-2">
              <Columns>
                <asp:TemplateField HeaderText="Descrição">
                  <ItemTemplate>
                    <input type="text" 
                           name="deb_desc" 
                           class="form-control form-control-sm" 
                           value='<%# Eval("Descricao") %>' />
                  </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Valor">
                  <ItemTemplate>
                    <input type="number" 
                           name="deb_valor" 
                           class="form-control form-control-sm text-end" 
                           value='<%# Eval("Valor") %>' 
                           min="0" step="0.01" />
                  </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Incidência">
                  <ItemTemplate>
                    <select name="deb_incid" 
                            class="form-select form-select-sm">
                      <option value="V" 
                        <%# Eval("FormaIncidencia").ToString()=="V" ? "selected" : "" %>>
                        Valor
                      </option>
                      <option value="P" 
                        <%# Eval("FormaIncidencia").ToString()=="P" ? "selected" : "" %>>
                        %
                      </option>
                    </select>
                  </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Ações">
                  <ItemTemplate>
                    <button type="button" 
                            class="btn btn-danger btn-sm" 
                            onclick="if(confirm('Confirma exclusão?')) this.closest('tr').remove();">
                      <i class="bi bi-trash"></i>
                    </button>
                  </ItemTemplate>
                </asp:TemplateField>
              </Columns>
            </asp:GridView>
            <button type="button" 
                    class="btn btn-outline-primary btn-sm mb-3" 
                    onclick="addRowSalario('<%= gridDebitos.ClientID %>')">
              + Adicionar Débito
            </button>
          </div>
        </ContentTemplate>
      </asp:UpdatePanel>

      <div class="modal-footer">
        <!-- dispara postback para btnSalvar_Click no code-behind -->
        <asp:Button 
          ID="btnSalvar" 
          runat="server" 
          Text="Salvar" 
          CssClass="btn btn-primary" 
          OnClick="btnSalvar_Click" />
        <button type="button" 
                class="btn btn-secondary" 
                data-bs-dismiss="modal">
          Cancelar
        </button>
      </div>

    </div>
  </div>
</div>
