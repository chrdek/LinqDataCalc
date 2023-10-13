//IIFE usage.
(function() {

let fngen=(function() {
return {
 UID: function uuidv4() {
  return `${btoa( ([1e7]+-1e3+-4e3+-8e3+-1e11).replace(/[018]/g, c =>
    (c ^ crypto.getRandomValues(new Uint8Array(1))[0] & 15 >> c / 4).toString(16)) )}`;
  }
}
})();

 setTimeout(function() {
   let generated = fngen.UID();

   const mTag_1 = document.createElement('meta');
   mTag_1.setAttribute('http-equiv','Content-Security-Policy');
   mTag_1.setAttribute('content',`script-src-elem 'nonce-${generated}' 'strict-dynamic'; style-src 'self';  frame-src 'none'; child-src 'none'`);
   document.head.prepend(mTag_1);

   const scrTag_2 = document.getElementById('script_test');
   scrTag_2.nonce =`${generated}`;

 },550);
})(); //IIFE end.