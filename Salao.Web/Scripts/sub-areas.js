$(function () {

    $("#Areas").change(function () {
        $.getJSON("/Admin/SubArea/GetSubAreas/" + $("#Areas > option:selected").attr("value"), function (data) {
            var itens = "<select id='SubAreas' name='SubAreas'>";
            $.each(data, function (i, subArea) {
                itens += "<option value='" + subArea.Value + "'>" + subArea.Text + "</option>";
            });
            itens += "</select>";
            $("#SubAreas").html(itens);
        });
    });

});