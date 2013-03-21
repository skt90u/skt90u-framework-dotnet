function refreshValidateCodeImage(img, src) {
    img = document.getElementById(img);
    
    if (!img || !src) return;
    
    img.src = src + '?code=' + randomNum(10);
    
    function randomNum(n){
        var rnd = '';
        for(var i=0; i<n; i++){
            rnd += Math.floor(Math.random()*10);
        }
        return rnd;
    }
}