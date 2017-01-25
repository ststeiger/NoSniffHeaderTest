<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="NoSniffHeaderTest._Default" %>

<!DOCTYPE html>
<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <style>
        a.Icon, div.Icon 
        {
            background-repeat: no-repeat;
            border: none;
            cursor: pointer;
            display: block;
            height: 16px;
            margin: auto auto;
            width: 16px;
        }

        .iRM 
        {
            background-image: url(images/room.gif);
        }

    </style>


</head>
<body>
    <form id="form1" runat="server">
        
        <h2>Various errors in various browsers</h2>

        <p>Problem: images/room.gif is actually a png file, and the nosniff-http-header is set</p>

        <p>IE: 1. No error message, if image is css URL (correctly outputs to console "image could not be decoded", but only on img.src, not when css-url)</p>
        <p>IE: 2. If the page was previously loaded with a correct room.gif (with mimetype gif) there is no more error.<br />
            It follows, IE Caches images and displays them if cached, although the http-headers clearly tell it not to cache anything at all, plus nosniff is ignored when cached</p>
        
        <p>Chrome: 1. nosniff is ignored ?</p> 
        <p>Chrome: 2. No message: wrong mime-type, resource interpreted as both in img.src as well as css-url</p>
        <p>Chrome: 3. This would be an excellent opportunity to see if no-caching is handled correctly, but can't test, because nosniff is ignored...</p> 

        <h5>Missing error messages, different behaviour in different browsers + caching-bugs - this renders testing into a futile endeavour, 
            and prolongs error-search beyond identifying &quot;the&quot; problem...</h5>

        <a target="_blank" href="https://msdn.microsoft.com/en-us/library/gg622941(v=vs.85).aspx">Reducing MIME type security risks</a><br />
        <a target="_blank" href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/X-Content-Type-Options">MDN: X-Content-Type-Options</a><br />
        <a target="_blank" href="https://wiki.mozilla.org/Security/Guidelines/Web_Security">Mozilla Web Security Guidelines</a><br />
        
        



        <br /><br /><br /><br />
        <hr />

        <div class="Icon iRM" onclick="alert('test');"></div>

        <hr />

        <img style="width: 16px; height: 16px;" src="images/room.gif" alt="Image with wrong file-extension" />
        
        <hr />

    </form>
</body>
</html>
