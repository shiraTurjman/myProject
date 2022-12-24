function ShowBig(Obj) {
   
    var myWrapper = document.querySelector('#Wrapper');
    var myBigImg = document.querySelector('#BigImg');
    myBigImg.src = Obj.getAttribute('src');  
    myWrapper.style.display = "inline"; 

}

function HideBig() {
    var myWrapper = document.querySelector('#Wrapper');
    myWrapper.style.display = "none";  
}