// JavaScript source code

/*$(document).ready(function (e) {
    $('.search-panel .dropdown-menu').find('a').click(function (e) {
        e.preventDefault();
        var param = $(this).attr("href").replace("#", "");
        var concept = $(this).text();
        $('.search-panel span#search_concept').text(concept);
        $('.input-group #search_param').val(param);
    });
});*/

/*
 * Fonctions anterieurs
 */
function estNumerique(intEstNumerique) {
    return !(isNaN(intEstNumerique) || isNaN(parseFloat(intEstNumerique, 10)))
}
function b(strIDBalise, strValeur) {
    var objBalise = document.getElementById(strIDBalise);
    if (!objBalise) {
        alert('Attention... balise ' + strIDBalise + ' inexistante !');
    }
    else {
        if (objBalise.value !== undefined) {
            /* Balise INPUT */
            if (strValeur !== undefined) {
                objBalise.value = strValeur;
            }
            else {
                return (objBalise.value);
            }
        }
        else {
            if (objBalise.src !== undefined) {
                /* Balise IMG */
                if (strValeur !== undefined) {
                    objBalise.src = strValeur;
                }
                else {
                    return (objBalise.src);
                }
            }
            else {
                /* Balise P ou SPAN */
                if (strValeur !== undefined) {
                    objBalise.innerHTML = strValeur;
                }
                else {
                    return (objBalise.innerHTML);
                }
            }
        }
    }
}