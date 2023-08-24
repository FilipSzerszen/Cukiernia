var oldCaretPos = 0;
var tBox = document.getElementById("Description");

function getId(obj) {
    if (!doGetCaretPosition(tBox) == 0) {
        oldCaretPos = doGetCaretPosition(tBox);
    }
    tBox.value = tBox.value.slice(0, oldCaretPos) + obj + tBox.value.slice(oldCaretPos)
}

function doGetCaretPosition(oField) {
    var iCaretPos = 0;

    // IE Support
    if (document.selection) {

        // Set focus on the element
        oField.focus();

        // To get cursor position, get empty selection range
        var oSel = document.selection.createRange();

        // Move selection start to 0 position
        oSel.moveStart('character', -oField.value.length);

        // The caret position is selection length
        iCaretPos = oSel.text.length;
    }

    // Firefox support
    else if (oField.selectionStart || oField.selectionStart == '0')
        iCaretPos = oField.selectionDirection == 'backward' ? oField.selectionStart : oField.selectionEnd;

    // Return results
    return iCaretPos;
}