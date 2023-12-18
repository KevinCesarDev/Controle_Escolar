
function mostrarTurma() {
    var divTurma = document.getElementById('divTurma');
    var contaAluno = document.getElementById('contaAluno');

    if(contaAluno.checked){
        divTurma.classList.remove('d-none');
    }else{
        divTurma.classList.add('d-none');
    }
}
