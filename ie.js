
window.onload = function() {
    function createCORSRequest(method, url){
        var xhr = new XMLHttpRequest();
        if ("withCredentials" in xhr){
            xhr.open(method, url, true);
        } else if (typeof XDomainRequest != "undefined"){
            xhr = new XDomainRequest();
            xhr.open(method, url);
        } else {
            xhr = null;
        }
        return xhr;
    }

    var request = createCORSRequest("get", "http://www.nczonline.net/");
    if (request){
        request.onload = function(){
            //do something with request.responseText
        };
        request.send();
    }
}