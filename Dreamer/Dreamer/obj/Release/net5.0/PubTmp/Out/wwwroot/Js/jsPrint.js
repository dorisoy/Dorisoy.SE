function Print() {
    $(".hideWhenPrint").hide();
    window.print();
    $(".hideWhenPrint").show();
}