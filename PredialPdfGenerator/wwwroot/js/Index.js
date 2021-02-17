$(document).ready(() => {
    // Choose the element that our invoice is rendered in.
    const element = document.getElementById("invoice");
    // Choose the element and save the PDF for our user.
    let opt = {
        margin: 1,
        filename: 'estadoPredial.pdf',
        image: { type: 'jpeg', quality: 0.98 },
        html2canvas: { scale: 1 },
        jsPDF: { unit: 'in', format: 'letter', orientation: 'portrait' }
    };

    html2pdf()
        .set(opt)
        .from(element)
        .save();
});

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