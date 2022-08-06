$(document).ready(function() {

    // Setup - add a text input to each footer cell
    $('#footer-search tfoot th').each(function() {
        var title = $(this).text();
        $(this).html('<input type="text" class="form-control" placeholder="Buscar ' + title + '" />');
    });

    // DataTable
    var table = $('#footer-search').DataTable();

    // Apply the search
    table.columns().every(function() {
        var that = this;

        $('input', this.footer()).on('keyup change', function() {
            if (that.search() !== this.value) {
                that
                    .search(this.value)
                    .draw();
            }
        });
    });
});
