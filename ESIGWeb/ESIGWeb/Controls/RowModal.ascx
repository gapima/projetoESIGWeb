<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RowModal.ascx.cs" Inherits="ESIGWeb.Controls.RowModal" %>

<div class="modal fade" id="rowModal" tabindex="-1" aria-labelledby="rowModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-xl">
    <div class="modal-content">

      <!-- Cabeçalho -->
      <div class="modal-header">
        <h5 class="modal-title" id="rowModalLabel">Detalhes da Pessoa</h5>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Fechar"></button>
      </div>

      <!-- Corpo -->
      <div class="modal-body">
        <ul class="nav nav-tabs" role="tablist">
          <li class="nav-item">
            <button class="nav-link active" data-bs-toggle="tab" data-bs-target="#panePessoa" type="button">Pessoa</button>
          </li>
          <li class="nav-item">
            <button class="nav-link" data-bs-toggle="tab" data-bs-target="#paneFinanceiro" type="button">Financeiro</button>
          </li>
        </ul>

        <div class="tab-content mt-3">

          <!-- Aba Pessoa -->
          <div class="tab-pane fade show active" id="panePessoa">
            <asp:ValidationSummary runat="server" CssClass="alert alert-danger" HeaderText="Corrija os erros abaixo:" />

            <div class="row gx-3 gy-2">

              <!-- Dados da Pessoa -->
              <div class="col-md-6">
                <h6>Dados da Pessoa</h6>

                <div class="mb-2 row">
                  <label class="col-sm-4 col-form-label" for="txtPessoaId">ID:</label>
                  <div class="col-sm-8">
                    <asp:TextBox ID="txtPessoaId" runat="server" CssClass="form-control" ReadOnly="true" />
                  </div>
                </div>

                <div class="mb-2 row">
                  <label class="col-sm-4 col-form-label" for="txtPessoaNome">Nome:</label>
                  <div class="col-sm-8">
                    <asp:TextBox ID="txtPessoaNome" runat="server" CssClass="form-control" />
                    <asp:RequiredFieldValidator 
                      ID="reqPessoaNome" 
                      runat="server" 
                      ControlToValidate="txtPessoaNome"
                      ErrorMessage="* Obrigatório" 
                      Display="Dynamic" 
                      CssClass="text-danger" />
                  </div>
                </div>

                <div class="mb-2 row">
                  <label class="col-sm-4 col-form-label" for="txtDataNascimento">Data Nasc.:</label>
                  <div class="col-sm-8">
                    <asp:TextBox ID="txtDataNascimento" runat="server" CssClass="form-control" TextMode="Date" />
                    <asp:RequiredFieldValidator 
                      ID="reqDataNascimento" 
                      runat="server" 
                      ControlToValidate="txtDataNascimento"
                      ErrorMessage="* Obrigatório" 
                      Display="Dynamic" 
                      CssClass="text-danger" />
                  </div>
                </div>

                <div class="mb-2 row">
                  <label class="col-sm-4 col-form-label" for="txtEmail">Email:</label>
                  <div class="col-sm-8">
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" />
                    <asp:RequiredFieldValidator 
                      ID="reqEmail" 
                      runat="server" 
                      ControlToValidate="txtEmail"
                      ErrorMessage="* Obrigatório" 
                      Display="Dynamic" 
                      CssClass="text-danger" />
                  </div>
                </div>

                <div class="mb-2 row">
                  <label class="col-sm-4 col-form-label" for="txtUsuario">Usuário:</label>
                  <div class="col-sm-8">
                    <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control" />
                  </div>
                </div>
              </div>

              <!-- Endereço & Contato -->
              <div class="col-md-6">
                <h6>Endereço & Contato</h6>

                <div class="mb-2 row">
                  <label class="col-sm-4 col-form-label" for="txtCidade">Cidade:</label>
                  <div class="col-sm-8">
                    <asp:TextBox ID="txtCidade" runat="server" CssClass="form-control" />
                  </div>
                </div>

                <div class="mb-2 row">
                  <label class="col-sm-4 col-form-label" for="txtCEP">CEP:</label>
                  <div class="col-sm-8">
                    <div class="input-group">
                      <asp:TextBox ID="txtCEP" runat="server" CssClass="form-control" />
                      <asp:Button 
                        ID="btnValidateCep" 
                        runat="server" 
                        Text="OK" 
                        CssClass="btn btn-outline-secondary" 
                        OnClientClick="return false;" />
                    </div>
                    <asp:RequiredFieldValidator
                      ID="reqCEP"
                      runat="server"
                      ControlToValidate="txtCEP"
                      ErrorMessage="* Obrigatório"
                      Display="Dynamic"
                      CssClass="text-danger" />
                  </div>
                </div>

                <div class="mb-2 row">
                  <label class="col-sm-4 col-form-label" for="txtEndereco">Endereço:</label>
                  <div class="col-sm-8">
                    <asp:TextBox ID="txtEndereco" runat="server" CssClass="form-control" />
                  </div>
                </div>

                <div class="mb-2 row">
                  <label class="col-sm-4 col-form-label" for="txtPais">País:</label>
                  <div class="col-sm-8">
                    <asp:TextBox ID="txtPais" runat="server" CssClass="form-control" />
                  </div>
                </div>

                <div class="mb-2 row">
                  <label class="col-sm-4 col-form-label" for="txtTelefone">Telefone:</label>
                  <div class="col-sm-8">
                    <asp:TextBox ID="txtTelefone" runat="server" CssClass="form-control" />
                  </div>
                </div>
              </div>
            </div>

            <!-- Combobox de Cargo -->
            <div class="row gx-3 gy-2 mt-3">
              <div class="col-md-6">
                <h6>Selecione o Cargo</h6>
                <div class="mb-2 row">
                  <label class="col-sm-4 col-form-label" for="ddlCargo">Cargo:</label>
                  <div class="col-sm-8">
                    <asp:DropDownList ID="ddlCargo" runat="server" CssClass="form-select" />
                  </div>
                </div>
              </div>
              <div class="col-md-6"></div>
            </div>
          </div>

        <!-- Aba Financeiro -->
        <div class="tab-pane fade" id="paneFinanceiro" role="tabpanel" aria-labelledby="tab-financeiro">
            <h6 class="mt-3">Créditos</h6>
            <asp:GridView
                ID="gridCreditos"
                runat="server"
                AutoGenerateColumns="false"
                ShowHeaderWhenEmpty="true"
                EmptyDataText="Nenhum crédito encontrado."
                CssClass="table table-sm table-striped table-bordered mb-4"
                GridLines="None">
            <Columns>
                <asp:BoundField DataField="Descricao" HeaderText="Descrição" />

                <asp:TemplateField HeaderText="Valor">
                <ItemTemplate>
                    <%# Eval("FormaIncidencia").ToString() == "P"
                        ? Eval("Valor", "{0:0.##}") + "%"
                        : Eval("Valor", "{0:C}") %>
                </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="FormaIncidencia" HeaderText="Incidência" />
            </Columns>
            </asp:GridView>

            <h6 class="mt-4">Débitos</h6>
            <asp:GridView
                ID="gridDebitos"
                runat="server"
                AutoGenerateColumns="false"
                ShowHeaderWhenEmpty="true"
                EmptyDataText="Nenhum débito encontrado."
                CssClass="table table-sm table-striped table-bordered"
                GridLines="None">
            <Columns>
                <asp:BoundField DataField="Descricao" HeaderText="Descrição" />

                <asp:TemplateField HeaderText="Valor">
                <ItemTemplate>
                    <%# Eval("FormaIncidencia").ToString() == "P"
                        ? Eval("Valor", "{0:0.##}") + "%"
                        : Eval("Valor", "{0:C}") %>
                </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="FormaIncidencia" HeaderText="Incidência" />
            </Columns>
            </asp:GridView>
        </div>


      <!-- Rodapé -->
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Fechar</button>
      </div>

    </div>
  </div>
</div>
