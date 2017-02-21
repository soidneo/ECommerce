﻿$(document).ready(function () {
    $("#NombreArchivo").html("Seleccione un archivo");
    $("#files").on("change", handleFileSelect);
});

function handleFileSelect(e) {
    var files = e.target.files;
    var filesArr = Array.prototype.slice.call(files);
    filesArr.forEach(function (f, item) {
        if (f.type.match("image.*")) {
            var reader = new FileReader();
            reader.readAsDataURL(f);

            $("#NombreArchivo.file-caption-name").val("lo qeu sea");
            $("#NombreArchivo").attr("title", f.name);

            $("#NombreArchivo").append("<span class='glyphicon glyphicon-file kv-caption-icon' style='display:inline-block'></span>" + f.name);
            document.getElementById("#NombreArchivo").innerHTML("Seleccione un archivo");
        }
        else {
            alert(f.name + ' no esta permitido cargarlo');
            return;
        }
    });
}
