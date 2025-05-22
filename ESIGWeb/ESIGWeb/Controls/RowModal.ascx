<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RowModal.ascx.cs" Inherits="ESIGWeb.Controls.RowModal" %>

<div class="modal fade" id="rowModal" tabindex="-1" aria-labelledby="rowModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-lg">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="rowModalLabel">Detalhes da Pessoa</h5>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Fechar"></button>
      </div>
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
          <div class="tab-pane fade show active" id="panePessoa">
            <div class="row">
              <div class="col-md-6">
                <h6>Dados da Pessoa</h6>
                <div class="mb-2"><strong>ID:</strong> <span id="lblPessoaId" runat="server"></span></div>
                <div class="mb-2"><strong>Nome:</strong> <span id="lblPessoaNome" runat="server"></span></div>
              </div>
              <div class="col-md-6">
                <h6>Dados do Cargo</h6>
                <div class="mb-2"><strong>Cargo ID:</strong> <span id="lblCargoId" runat="server"></span></div>
                <div class="mb-2"><strong>Nome do Cargo:</strong> <span id="lblCargoNome" runat="server"></span></div>
              </div>
            </div>
          </div>
          <div class="tab-pane fade" id="paneFinanceiro">
            <p class="text-muted">Detalhes financeiros estarão disponíveis em breve.</p>
          </div>
        </div>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Fechar</button>
      </div>
    </div>
  </div>
</div>
