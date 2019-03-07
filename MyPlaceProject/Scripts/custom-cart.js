$(document).ready(function () {
    var total = parseInt($('p .total').html());
    var counter = 0;
    $('input[name="DetailCommande.Index"]').each(function () {
        counter++;
    });
    $('#list-menu li').draggable({
        revert: "invalid",
        helper: 'clone'
    });
    $('#panier').droppable({
        accept: '#list-menu li',
        drop: function (e, source) {
            add(e, source.draggable);
        }
    });
    function add(e, source) {
        var row = $('#panier table>tbody tr').filter(function () {
            return $(this).data('id') == source.data('id');
        });
        if (row.length > 0) {
            var quantite = row.find('input[name*="Quantite"]');
            quantite.val(parseInt(quantite.val()) + 1);
            row.find('.total').html(quantite.val()*row.data('prix'));
        } else {
            addrow(source);
        }
        total += source.data('prix');
        updateTotal();
    }
    function addrow(source) {
        var newRow = $("<tr>").data('id', parseInt(source.data('id'))).data('prix', parseInt(source.data('prix')));
        var cols = "";
        
        cols += '<td> <input type="hidden" name="DetailCommande.Index" value="' + counter + '"/>' + source.data('nom') + '</td>';
        cols += '<td> <input type="hidden" name="DetailCommande[' + counter + '].ProduitId" value="' + source.data('id') + '"/>' + source.data('categorie') + '</td>';
        cols += '<td> <input type="hidden" name="DetailCommande[' + counter + '].PrixUnitaire" value="' + source.data('prix') + '"/>' + source.data('prix') + '</td>';
        cols += '<td class="col-sm-2"><input type="number" class="form-control" name="DetailCommande[' + counter + '].Quantite" value="1" min="0" /></td>';

        cols += '<td class="total">' + source.data('prix') + '</td>';
        cols += '<td><input type="button" class="delete-btn btn btn-md btn-danger" value="Supprimer"></td>';
        newRow.append(cols);
        $("#panier table>tbody").append(newRow);
        counter++;
    }
    function updateTotal() {
        $('p .total').html(total);
    }

    $("#panier table>tbody").on("click", ".delete-btn", function (event) {
        $(this).closest("tr").remove();
        calculateGrandTotal();
    });
    $("#panier table>tbody").on("change", "input[name*=Quantite]", function (event) {
        if ($(this).val() <= 0) {
            $(this).closest("tr").remove();
        }
        calculateGrandTotal();
    });

    function calculateGrandTotal() {
        total = 0;
        $("#panier table>tbody tr").each(function () {
            value = Math.max($(this).data('prix') * parseFloat($(this).find('input[name*="Quantite"]').val()), 0);
            $(this).find('.total').html(value);
            total += value;
        });
        updateTotal();
    }
});