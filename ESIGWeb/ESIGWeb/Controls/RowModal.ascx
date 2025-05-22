<%@ Control Language="C#" AutoEventWireup="true" %>

<!-- UserControl: RowModal.ascx -->
<div class="modal fade" id="rowModal" tabindex="-1" aria-labelledby="rowModalLabel" aria-hidden="true">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="rowModalLabel">Detalhes da Pessoa</h5>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Fechar"></button>
      </div>
      <div class="modal-body">
        <!-- Conteúdo a ser implementado -->
        <p>Aqui serão exibidos os detalhes da pessoa selecionada.</p>
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