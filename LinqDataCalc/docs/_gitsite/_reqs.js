let mainpanel = [
'https://www.nuget.org/api/v2/Search()?$filter=IsLatestVersion&searchTerm=%27owner%3Achrkqedek%27&targetFramework=%27%27&includePrerelease=false&$skip=0&$top=5&semVerLevel=2.0.0'
];

let dataFeeds = [
'https://codepen.io/picks/feed/',
'https://caniuse.com/feed/',
'https://caniuse.com/feed/?search='
];

(function(url) {

url = mainpanel[0]; //dataFeeds[0];
 const xhr = new XMLHttpRequest()
 xhr.open( 'GET', url);
 xhr.setRequestHeader('Access-Control-Allow-Headers', '*');
 xhr.setRequestHeader('Access-Control-Allow-Origin', '*');
 xhr.setRequestHeader('Access-Control-Allow-Methods','GET');
 xhr.setRequestHeader('Access-Control-Allow-Headers','Origin, Content-Type, Accept, Authorization, X-Request-With');
 xhr.withCredentials = false;
 xhr.onreadystatechange = function() {
    console.log( xhr.status, xhr.statusText )
}
xhr.send(null);

})();