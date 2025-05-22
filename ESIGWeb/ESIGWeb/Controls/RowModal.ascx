<%@ Control Language="C#" AutoEventWireup="true" %>

<!-- UserControl: RowModal.ascx -->
<div class="modal fade" id="rowModal" tabindex="-1" aria-labelledby="rowModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-lg">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="rowModalLabel">Detalhes da Pessoa</h5>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Fechar"></button>
      </div>
      <div class="modal-body">
        <!-- Nav tabs -->
        <ul class="nav nav-tabs" id="rowModalTab" role="tablist">
          <li class="nav-item" role="presentation">
            <button class="nav-link active" id="tab-pessoa" data-bs-toggle="tab" data-bs-target="#panePessoa" type="button" role="tab" aria-controls="panePessoa" aria-selected="true">Pessoa</button>
          </li>
          <li class="nav-item" role="presentation">
            <button class="nav-link" id="tab-financeiro" data-bs-toggle="tab" data-bs-target="#paneFinanceiro" type="button" role="tab" aria-controls="paneFinanceiro" aria-selected="false">Financeiro</button>
          </li>
        </ul>
        <!-- Tab panes -->
        <div class="tab-content mt-3">
          <!-- Aba Pessoa -->
          <div class="tab-pane fade show active" id="panePessoa" role="tabpanel" aria-labelledby="tab-pessoa">
            <div class="row">
              <div class="col-md-6">
                <h6>Dados da Pessoa</h6>
                <div class="mb-2"><strong>ID:</strong> <span id="lblPessoaId"></span></div>
                <div class="mb-2"><strong>Nome:</strong> <span id="lblPessoaNome"></span></div>
              </div>
              <div class="col-md-6">
                <h6>Dados do Cargo</h6>
                <div class="mb-2"><strong>Cargo ID:</strong> <span id="lblCargoId"></span></div>
                <div class="mb-2"><strong>Nome do Cargo:</strong> <span id="lblCargoNome"></span></div>
              </div>
            </div>
          </div>
          <!-- Aba Financeiro -->
          <div class="tab-pane fade" id="paneFinanceiro" role="tabpanel" aria-labelledby="tab-financeiro">
            <!-- Conteúdo financeiro vazio por enquanto -->
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

<script>
    // Script para abrir a modal
    document.addEventListener('showRowModal', function () {
        var modalEl = document.getElementById('rowModal');
        var modal = new bootstrap.Modal(modalEl);
        modal.show();
    });
</script>
