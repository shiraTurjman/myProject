
var turn;
function gamedata() {
    
    var player1 = document.querySelector("#play1").value;
    var player2 = document.querySelector("#play2").value;
    if (player1.length == 0 || player2.length == 0) {
        document.querySelector("#alerterror").textContent = "יש להכניס 2 שמות";
    }
    else
        if (player1 == player2) {
            document.querySelector("#alerterror").textContent = "יש להכניס 2 שמות שונים ";
        }
        else {
            sessionStorage.player1 = document.querySelector("#play1").value;
            sessionStorage.player2 = document.querySelector("#play2").value;
      
            
            window.location = "theGame.html";
        }
}


function game() {
    var mat = [[0, 0, 0], [0, 0, 0], [0, 0, 0]];

    var flag = true;
    var x = 0;
   var play1 = sessionStorage.player1;
    var play2 = sessionStorage.player2;
    document.querySelector("#namePlay").textContent = play1 + "," + play2;
    turn = parseInt(Math.random() * 2 + 1, 10);
   
    document.getElementById("yourTurn").textContent = "עכשו תורך" + " " + sessionStorage.getItem("player" + turn);
  

    for (var line = 0; line < 3; line++) {
        for (var column = 0; column < 3; column++) {

            var klaf = document.createElement("img");
            
            document.querySelector("#mat").appendChild(klaf);
            klaf.src = "../img/images.jpg";
           

            klaf.setAttribute("data-line", line);
            klaf.setAttribute("data-column", column);
            
            klaf.setAttribute("id", "aaa");
                                                        
            klaf.onclick = function (k) {
                   
                    var l = k.target.getAttribute("data-line");
                var c = k.target.getAttribute("data-column");
                var idd = k.target.getAttribute("id");
                if (mat[l][c] == 0 && flag == true) {
                    x++;
                       
                    k.target.src = "../img/win" + turn + ".jpg";
                
                        var a = parseInt(l,10);
                        var b = parseInt(c,10);
                        if (turn == 1)
                            mat[l][c] = 1;
                        else
                            mat[l][c] = 10;

                      
                        var count = mat[l][c];
                        for (var i = 0; i < 2; i++) {
                            a++;
                            if (a > 2) a = 0;
                            count = count + mat[a][c];
                        }
                        if (count == 3 || count == 30) {
                            document.querySelector("#yourTurn").textContent = "!!!ניצחת, כל הכבוד"+" "+ sessionStorage.getItem("player" + turn)  ;
                            flag = false;
                                for (var i = 0; i <3; i++) {
                                    document.querySelector(`[data-line="${i}"][data-column="${c}"]`).src = "../img/clapping_hands.gif";
                        

                            }
                        }
                        else {
                            count = mat[l][c];
                            for (var i = 0; i < 2; i++) {
                                b++;
                                if (b > 2) b = 0;
                                count = count + mat[l][b];
                            }
                            if (count == 3 || count == 30) {
                                document.querySelector("#yourTurn").textContent = "!!!"+sessionStorage.getItem("player" + turn) + " " +  "ניצחת, כל הכבוד"  ;
                                flag = false;
                                for (var i = 0; i < 3; i++) {
                                    document.querySelector(`[data-line="${l}"][data-column="${i}"]`).src = "../img/clapping_hands.gif";
                                }

                            }
                            else {
                                
                                count = mat[l][c];
                                a = l;
                                b = c;
                                for (var i = 0; i < 2; i++) {
                                    a++; b++;
                                    if (a > 2) a = 0;
                                    if (b > 2) b = 0;
                                    if(a==b||(a+b)==2)
                                    count = count + mat[a][b];
                                }
                                if (count == 3 || count == 30) {
                                    document.querySelector("#yourTurn").textContent = sessionStorage.getItem("player" + turn) + " " +  "!!!ניצחת, כל הכבוד" ;
                                    flag = false;
                                    for (var i = 0; i < 3; i++) {
                                       
                                            document.querySelector(`[data-line="${i}"][data-column="${i}"]`).src = "../img/clapping_hands.gif";

                                    }

                                }
                                else {
                                    count = mat[l][c];
                                    a = l;
                                    b = c;
                                    for (var i = 0; i < 2; i++) {
                                        a++; b--;
                                        if (a > 2) a = 0;
                                        if (b < 0) b = 2;
                                        if (a == b || (a + b) == 2)
                                        count = count + mat[a][b];
                                    }
                                    if (count == 3 || count == 30) {
                                        document.querySelector("#yourTurn").textContent = sessionStorage.getItem("player" + turn) + " " + "!!!ניצחת, כל הכבוד" ;
                                        flag = false;
                                        a = 0; b = 2;
                                        for (var i = 0; i < 3; i++) {
                                            
                                            document.querySelector(`[data-line="${a}"][data-column="${b}"]`).src = "../img/clapping_hands.gif";
                                            a++; b--;
                                            if (a > 2) a = 0;
                                            if (b > 2) b = 0;
                                           

                                        }
                                    }

                                }
                            }
                        } if (flag == true) {
                            turn = turn % 2 + 1;
                            document.getElementById("yourTurn").textContent = "עכשו תורך" + " " + sessionStorage.getItem("player" + turn);

                            if (x == 9) {
                                document.querySelector("#yourTurn").textContent = " .....תיקו ,שחקו שוב";
                                var q = 0; var w = 0; var t = 0; var y;
                                y = setInterval(function () {
                                    if (q < 9) {
                                        document.querySelector(`[data-line="${w}"][data-column="${t}"]`).src = "../img/images.jpg";
                                        q++;
                                        t++;
                                        if (t > 2) { w++; t = 0; }
                                    }
                                    else {
                                        clearInterval(x);
                                     
                                    }
                                }, 250)
                             
                               

                            }
                           
                            
                        }
                    }
                    else
                    if (flag == true) {
                        if (x == 9)
                            alert("אתחל את משחק")
                        else
                        alert("מקום זה תפוס ,בחר מקום אחר");

                        }
                }
            }



        
    }
   

} 

function newGame() {
    window.location = "home.html";

}
function resetGame() {
    window.location = "theGame.html";
}


