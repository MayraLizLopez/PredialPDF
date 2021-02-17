let value = $("#referenciaOxxo").html();
var btype = "code128";
var renderer = "css";

var quietZone = false;

var settings = {
    output: renderer,
    bgColor: "#FFFFFF",
    color: "#000000",
    barWidth: 1.7,
    barHeight: 50,
};

if (renderer == 'canvas') {
    clearCanvas();
    $("#barcodeTarget").hide();
    $("#canvasTarget").show().barcode(value, btype, settings);
} else {
    $("#canvasTarget").hide();
    $("#barcodeTarget").html("").show().barcode(value, btype, settings);
}