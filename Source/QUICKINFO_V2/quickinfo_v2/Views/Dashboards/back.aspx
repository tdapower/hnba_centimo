<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="back.aspx.cs" Inherits="quickinfo_v2.Views.Dashboards.back" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8">
    <title>CSS 3D Clouds</title>

    <script type="text/javascript" async="" src="../../Scripts/css3clouds/plusone.js"></script>
    <script type="text/javascript" async="" src="../../Scripts/css3clouds/ga.js"></script>
    <script async="" src="../../Scripts/css3clouds/cloudflare.min.js"></script>


    <script type="text/javascript">
        //<![CDATA[
        try {
            if (!window.CloudFlare) {
                var CloudFlare = [{
                    verbose: 0, p: 0, byc: 0, owlid: "cf", bag2: 1, mirage2: 0, oracle: 0, paths:
                        { cloudflare: "/cdn-cgi/nexp/dok3v=1613a3a185/" }, atok: "b5e3c6bd65be78ed83793421c2996ba1",
                        petok: "ac072429612820343918bacfd0b614aa19c7ef68-1474517903-1800", zone: "clicktorelease.com", rocket: "0",
                        apps: { "ga_key": { "ua": "UA-625393-26", "ga_bs": "2" }, "abetterbrowser": { "ie": "7" } }, sha2test: 0
                }]; !function (a, b) {
                    a = document.createElement("script"), b = document.getElementsByTagName("script")[0],
                        a.async = !0, a.src = "../../Scripts/css3clouds/cloudflare.min.js", b.parentNode.insertBefore(a, b)
                }()
            }
        } catch (e) { };
        //]]>
    </script>

    <style>
        * {
            box-sizing: border-box;
            margin: 0;
            padding: 0;
        }

        body {
            color: #eee;
            text-shadow: 0 -1px 0 rgba( 0, 0, 0, .6 );
            font-family: 'Open Sans', sans-serif;
            font-size: 13px;
            line-height: 16px;
            overflow: hidden;
        }

        #viewport {
            -webkit-perspective: 1000;
            -moz-perspective: 1000px;
            -o-perspective: 1000;
            perspective: 1000px;
            position: absolute;
            left: 0;
            top: 0;
            right: 0;
            bottom: 0;
            overflow: hidden;
            background-image: linear-gradient(bottom, rgb(69,132,180) 28%, rgb(31,71,120) 64%);
            background-image: -o-linear-gradient(bottom, rgb(69,132,180) 28%, rgb(31,71,120) 64%);
            background-image: -moz-linear-gradient(bottom, rgb(69,132,180) 28%, rgb(31,71,120) 64%);
            background-image: -webkit-linear-gradient(bottom, rgb(69,132,180) 28%, rgb(31,71,120) 64%);
            background-image: -ms-linear-gradient(bottom, rgb(69,132,180) 28%, rgb(31,71,120) 64%);
            background-image: -webkit-gradient( linear, left bottom, left top, color-stop(0.28, rgb(69,132,180)), color-stop(0.64, rgb(31,71,120)) );
        }

        #world {
            position: absolute;
            left: 50%;
            top: 50%;
            margin-left: -256px;
            margin-top: -256px;
            height: 512px;
            width: 512px;
            //border: 1px solid rgb( 255, 0, 0 );
            -webkit-transform-style: preserve-3d;
            -moz-transform-style: preserve-3d;
            -o-transform-style: preserve-3d;
            transform-style: preserve-3d;
            pointer-events: none;
        }

            #world div {
                -webkit-transform-style: preserve-3d;
                -moz-transform-style: preserve-3d;
                -o-transform-style: preserve-3d;
                transform-style: preserve-3d;
            }

        .cloudBase {
            //border: 1px solid #ff00ff;
            position: absolute;
            left: 256px;
            top: 256px;
            width: 20px;
            height: 20px;
            margin-left: -10px;
            margin-top: -10px;
        }

        .cloudLayer {
            position: absolute;
            left: 50%;
            top: 50%;
            width: 256px;
            height: 256px;
            margin-left: -128px;
            margin-top: -128px;
            -webkit-transition: opacity .5s ease-out;
            -moz-transition: opacity .5s ease-out;
            -o-transition: opacity .5s ease-out;
            transition: opacity .5s ease-out;
        }

        #options {
            position: absolute;
            left: 0;
            top: 0;
            margin: 10px;
            padding: 20px;
            width: 400px;
            background-color: rgba( 0, 0, 0, .4 );
            border-radius: 5px;
        }

        #optionsContent {
            margin-top: 20px;
            -webkit-transition: all 1s ease-out;
            -moz-transition: all 1s ease-out;
            -o-transition: all 1s ease-out;
            transition: all 1s ease-out;
        }

        h1 {
            font-family: 'Lato', sans-serif;
        }

        h2 {
            font-family: 'Lato', sans-serif;
            margin-bottom: 10px;
        }

        p {
            margin-bottom: 20px;
        }

        .actions {
            margin-bottom: 20px;
        }

        #textureList li {
            clear: both;
            list-style-type: none;
            position: relative;
            height: 35px;
            padding-top: 10px;
        }

            #textureList li span {
                text-transform: capitalize;
            }

        #textureList div {
            position: absolute;
            right: 0;
            top: 0;
        }

        #textureList li a {
            float: left;
        }

        a {
            color: inherit;
        }

        #textureControls {
            display: none;
        }



        .presets {
        }

            .presets a {
                float: left;
            }

        .nope {
            text-decoration: line-through;
        }

        :-moz-full-screen #options {
            display: none;
        }

        :-webkit-full-screen #options {
            display: none;
        }

        :full-screen #options {
            display: none;
        }
    </style>
    <script type="text/javascript">
        /* <![CDATA[ */
        var _gaq = _gaq || [];
        _gaq.push(['_setAccount', 'UA-625393-26']);
        _gaq.push(['_trackPageview']);

        (function () {


        (function (b) { (function (a) { "__CF" in b && "DJS" in b.__CF ? b.__CF.DJS.push(a) : "addEventListener" in b ? b.addEventListener("load", a, !1) : b.attachEvent("onload", a) })(function () { "FB" in b && "Event" in FB && "subscribe" in FB.Event && (FB.Event.subscribe("edge.create", function (a) { _gaq.push(["_trackSocial", "facebook", "like", a]) }), FB.Event.subscribe("edge.remove", function (a) { _gaq.push(["_trackSocial", "facebook", "unlike", a]) }), FB.Event.subscribe("message.send", function (a) { _gaq.push(["_trackSocial", "facebook", "send", a]) })); "twttr" in b && "events" in twttr && "bind" in twttr.events && twttr.events.bind("tweet", function (a) { if (a) { var b; if (a.target && a.target.nodeName == "IFRAME") a: { if (a = a.target.src) { a = a.split("#")[0].match(/[^?=&]+=([^&]*)?/g); b = 0; for (var c; c = a[b]; ++b) if (c.indexOf("url") === 0) { b = unescape(c.split("=")[1]); break a } } b = void 0 } _gaq.push(["_trackSocial", "twitter", "tweet", b]) } }) }) })(window);
        /* ]]> */
    </script>
    <style type="text/css">
        .cf-hidden {
            display: none;
        }

        .cf-invisible {
            visibility: hidden;
        }
    </style>
</head>
<body style="width: 1366px; height: 768px;">

    <div id="viewport" style="perspective: 400px;">
        <div id="world" style="transform: translateZ(-30px) rotateX(-15.4054deg) rotateY(-56.0029deg);">
            <div class="cloudBase" style="transform: translateX(51.0219px) translateY(-77.7636px) translateZ(-13.6974px);">
                <img src="../../Styles/css3clouds/cloud.png" class="cloudLayer" style="opacity: 0.8; transform: translateX(1.65075px) translateY(9.3489px) translateZ(46.5568px) rotateY(56.0029deg) rotateX(15.4054deg) rotateZ(2596.19deg) scale(0.728337);"><img src="../../Styles/css3clouds/cloud.png" class="cloudLayer" style="opacity: 0.8; transform: translateX(6.72677px) translateY(-44.2033px) translateZ(-81.849px) rotateY(56.0029deg) rotateX(15.4054deg) rotateZ(805.414deg) scale(0.352076);"><img src="../../Styles/css3clouds/cloud.png" class="cloudLayer" style="opacity: 0.8; transform: translateX(45.009px) translateY(-6.50493px) translateZ(-91.4978px) rotateY(56.0029deg) rotateX(15.4054deg) rotateZ(1074.87deg) scale(0.513283);"><img src="../../Styles/css3clouds/cloud.png" class="cloudLayer" style="opacity: 0.8; transform: translateX(-9.02573px) translateY(11.2207px) translateZ(-47.2438px) rotateY(56.0029deg) rotateX(15.4054deg) rotateZ(2212.24deg) scale(0.456468);"><img src="../../Styles/css3clouds/cloud.png" class="cloudLayer" style="opacity: 0.8; transform: translateX(-33.0538px) translateY(48.2264px) translateZ(-0.954405px) rotateY(56.0029deg) rotateX(15.4054deg) rotateZ(1716.5deg) scale(1.19571);">
            </div>
            <div class="cloudBase" style="transform: translateX(4.47155px) translateY(-87.8145px) translateZ(175.239px);">
                <img src="../../Styles/css3clouds/cloud.png" class="cloudLayer" style="opacity: 0.8; transform: translateX(17.4891px) translateY(-43.8312px) translateZ(45.3598px) rotateY(56.0029deg) rotateX(15.4054deg) rotateZ(906.079deg) scale(0.369925);"><img src="../../Styles/css3clouds/cloud.png" class="cloudLayer" style="opacity: 0.8; transform: translateX(6.28088px) translateY(1.12407px) translateZ(-8.57669px) rotateY(56.0029deg) rotateX(15.4054deg) rotateZ(169.906deg) scale(0.69836);"><img src="../../Styles/css3clouds/cloud.png" class="cloudLayer" style="opacity: 0.8; transform: translateX(-14.4046px) translateY(26.0835px) translateZ(21.135px) rotateY(56.0029deg) rotateX(15.4054deg) rotateZ(1587.64deg) scale(1.14807);"><img src="../../Styles/css3clouds/cloud.png" class="cloudLayer" style="opacity: 0.8; transform: translateX(42.5907px) translateY(-4.28649px) translateZ(-55.1966px) rotateY(56.0029deg) rotateX(15.4054deg) rotateZ(1387.46deg) scale(0.494373);"><img src="../../Styles/css3clouds/cloud.png" class="cloudLayer" style="opacity: 0.8; transform: translateX(-21.2056px) translateY(46.9852px) translateZ(-51.5525px) rotateY(56.0029deg) rotateX(15.4054deg) rotateZ(1456.37deg) scale(0.468283);"><img src="../../Styles/css3clouds/cloud.png" class="cloudLayer" style="opacity: 0.8; transform: translateX(5.7372px) translateY(16.2129px) translateZ(-18.7988px) rotateY(56.0029deg) rotateX(15.4054deg) rotateZ(961.706deg) scale(0.717435);"><img src="../../Styles/css3clouds/cloud.png" class="cloudLayer" style="opacity: 0.8; transform: translateX(16.992px) translateY(-38.0523px) translateZ(-71.5832px) rotateY(56.0029deg) rotateX(15.4054deg) rotateZ(1233.99deg) scale(1.18378);"><img src="../../Styles/css3clouds/cloud.png" class="cloudLayer" style="opacity: 0.8; transform: translateX(-29.9548px) translateY(-20.4695px) translateZ(75.5225px) rotateY(56.0029deg) rotateX(15.4054deg) rotateZ(642.153deg) scale(1.09011);">
            </div>
            <div class="cloudBase" style="transform: translateX(-0.758252px) translateY(-1.9945px) translateZ(120.309px);">
                <img src="../../Styles/css3clouds/cloud.png" class="cloudLayer" style="opacity: 0.8; transform: translateX(35.5745px) translateY(-48.5196px) translateZ(72.3128px) rotateY(56.0029deg) rotateX(15.4054deg) rotateZ(1436.07deg) scale(0.633402);"><img src="../../Styles/css3clouds/cloud.png" class="cloudLayer" style="opacity: 0.8; transform: translateX(-44.4511px) translateY(-38.5221px) translateZ(45.5152px) rotateY(56.0029deg) rotateX(15.4054deg) rotateZ(2328.06deg) scale(0.721163);"><img src="../../Styles/css3clouds/cloud.png" class="cloudLayer" style="opacity: 0.8; transform: translateX(17.4661px) translateY(-33.37px) translateZ(-71.4811px) rotateY(56.0029deg) rotateX(15.4054deg) rotateZ(2483.35deg) scale(0.985936);"><img src="../../Styles/css3clouds/cloud.png" class="cloudLayer" style="opacity: 0.8; transform: translateX(16.2912px) translateY(-34.254px) translateZ(42.448px) rotateY(56.0029deg) rotateX(15.4054deg) rotateZ(870.1deg) scale(1.13013);"><img src="../../Styles/css3clouds/cloud.png" class="cloudLayer" style="opacity: 0.8; transform: translateX(-37.901px) translateY(-0.300386px) translateZ(75.0896px) rotateY(56.0029deg) rotateX(15.4054deg) rotateZ(1860.32deg) scale(0.273096);"><img src="../../Styles/css3clouds/cloud.png" class="cloudLayer" style="opacity: 0.8; transform: translateX(-16.8873px) translateY(-14.1129px) translateZ(-25.1228px) rotateY(56.0029deg) rotateX(15.4054deg) rotateZ(1615.11deg) scale(1.03171);"><img src="../../Styles/css3clouds/cloud.png" class="cloudLayer" style="opacity: 0.8; transform: translateX(23.2035px) translateY(1.62382px) translateZ(46.5491px) rotateY(56.0029deg) rotateX(15.4054deg) rotateZ(1320.92deg) scale(0.285129);"><img src="../../Styles/css3clouds/cloud.png" class="cloudLayer" style="opacity: 0.8; transform: translateX(-4.17257px) translateY(32.2992px) translateZ(29.119px) rotateY(56.0029deg) rotateX(15.4054deg) rotateZ(631.262deg) scale(0.809131);">
            </div>
            <div class="cloudBase" style="transform: translateX(103.07px) translateY(213.886px) translateZ(-110.868px);">
                <img src="../../Styles/css3clouds/cloud.png" class="cloudLayer" style="opacity: 0.8; transform: translateX(-27.5237px) translateY(34.8904px) translateZ(-44.1819px) rotateY(56.0029deg) rotateX(15.4054deg) rotateZ(341.492deg) scale(0.784258);"><img src="../../Styles/css3clouds/cloud.png" class="cloudLayer" style="opacity: 0.8; transform: translateX(49.3011px) translateY(17.5793px) translateZ(-0.0543167px) rotateY(56.0029deg) rotateX(15.4054deg) rotateZ(190.165deg) scale(0.727721);"><img src="../../Styles/css3clouds/cloud.png" class="cloudLayer" style="opacity: 0.8; transform: translateX(22.2012px) translateY(-13.8872px) translateZ(80.2773px) rotateY(56.0029deg) rotateX(15.4054deg) rotateZ(1794.25deg) scale(0.337456);"><img src="../../Styles/css3clouds/cloud.png" class="cloudLayer" style="opacity: 0.8; transform: translateX(26.5074px) translateY(50.2141px) translateZ(-2.6176px) rotateY(56.0029deg) rotateX(15.4054deg) rotateZ(1531.1deg) scale(0.431681);"><img src="../../Styles/css3clouds/cloud.png" class="cloudLayer" style="opacity: 0.8; transform: translateX(-47.1959px) translateY(-45.5973px) translateZ(41.973px) rotateY(56.0029deg) rotateX(15.4054deg) rotateZ(298.71deg) scale(0.820751);">
            </div>
            <div class="cloudBase" style="transform: translateX(-246.793px) translateY(128.927px) translateZ(22.9491px);">
                <img src="../../Styles/css3clouds/cloud.png" class="cloudLayer" style="opacity: 0.8; transform: translateX(-20.2108px) translateY(-38.5682px) translateZ(74.859px) rotateY(56.0029deg) rotateX(15.4054deg) rotateZ(1792.5deg) scale(0.701953);"><img src="../../Styles/css3clouds/cloud.png" class="cloudLayer" style="opacity: 0.8; transform: translateX(29.8135px) translateY(-2.65086px) translateZ(90.7526px) rotateY(56.0029deg) rotateX(15.4054deg) rotateZ(1813.32deg) scale(0.679564);"><img src="../../Styles/css3clouds/cloud.png" class="cloudLayer" style="opacity: 0.8; transform: translateX(15.6799px) translateY(38.2567px) translateZ(14.9799px) rotateY(56.0029deg) rotateX(15.4054deg) rotateZ(2649.67deg) scale(1.21812);"><img src="../../Styles/css3clouds/cloud.png" class="cloudLayer" style="opacity: 0.8; transform: translateX(-14.441px) translateY(-1.11492px) translateZ(-92.9158px) rotateY(56.0029deg) rotateX(15.4054deg) rotateZ(1695.42deg) scale(0.462323);"><img src="../../Styles/css3clouds/cloud.png" class="cloudLayer" style="opacity: 0.8; transform: translateX(16.5066px) translateY(0.115144px) translateZ(62.2918px) rotateY(56.0029deg) rotateX(15.4054deg) rotateZ(1969.99deg) scale(0.290246);"><img src="../../Styles/css3clouds/cloud.png" class="cloudLayer" style="opacity: 0.8; transform: translateX(45.272px) translateY(38.6707px) translateZ(68.8417px) rotateY(56.0029deg) rotateX(15.4054deg) rotateZ(1931.43deg) scale(0.677206);"><img src="../../Styles/css3clouds/cloud.png" class="cloudLayer" style="opacity: 0.8; transform: translateX(27.9559px) translateY(-22.1038px) translateZ(28.4326px) rotateY(56.0029deg) rotateX(15.4054deg) rotateZ(1351.25deg) scale(1.03197);">
            </div>
        </div>
    </div>



    <div id="options" style="display: none;">

        <div id="optionsContent" style="display: none;">


            <div id="textureControls" style="display: none;">
                <ul id="textureList">
                  
                    
                  
                   
                  
                 
                </ul>
            </div>



        </div>
    </div>

    <script>

        function getParameterByName(name) {
            name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
            var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
                results = regex.exec(location.search);
            return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
        }

        var isKosher = getParameterByName('metadata').indexOf('Player') === -1;

        (function () {
            var lastTime = 0;
            var vendors = ['ms', 'moz', 'webkit', 'o'];
            for (var x = 0; x < vendors.length && !window.requestAnimationFrame; ++x) {
                window.requestAnimationFrame = window[vendors[x] + 'RequestAnimationFrame'];
                window.cancelRequestAnimationFrame = window[vendors[x] +
                  'CancelRequestAnimationFrame'];
            }

            if (!window.requestAnimationFrame)
                window.requestAnimationFrame = function (callback, element) {
                    var currTime = new Date().getTime();
                    var timeToCall = Math.max(0, 16 - (currTime - lastTime));
                    var id = window.setTimeout(function () { callback(currTime + timeToCall); },
                      timeToCall);
                    lastTime = currTime + timeToCall;
                    return id;
                };

            if (!window.cancelAnimationFrame)
                window.cancelAnimationFrame = function (id) {
                    clearTimeout(id);
                };
        }())

        var layers = [],
            objects = [],
            textures = [],

            world = document.getElementById('world'),
            viewport = document.getElementById('viewport'),

            d = 0,
            p = 400,
            worldXAngle = 0,
            worldYAngle = 0,
            computedWeights = [];

        var links = document.querySelectorAll('a[rel=external]');
        for (var j = 0; j < links.length; j++) {
            var a = links[j];
            a.addEventListener('click', function (e) {
                window.open(this.href, '_blank');
                e.preventDefault();
            }, false);
        }

        viewport.style.webkitPerspective = p;
        viewport.style.MozPerspective = p + 'px';
        viewport.style.oPerspective = p;
        viewport.style.perspective = p;

        textures = [
            { name: 'white cloud', file: '../../Styles/css3clouds/cloud.png', opacity: 1, weight: 0 },
            { name: 'dark cloud', file: 'darkCloud.png', opacity: 1, weight: 0 },
            { name: 'smoke cloud', file: 'smoke.png', opacity: 1, weight: 0 },
            { name: 'explosion', file: 'explosion.png', opacity: 1, weight: 0 },
            { name: 'explosion 2', file: 'explosion2.png', opacity: 1, weight: 0 },
            { name: 'box', file: 'box.png', opacity: 1, weight: 0 }
        ];

        var el = document.getElementById('textureList');
        for (var j = 0; j < textures.length; j++) {
            var li = document.createElement('li');
            var span = document.createElement('span');
            span.textContent = textures[j].name;
            var div = document.createElement('div');
            div.className = 'buttons';
            var btnNone = document.createElement('a');
            btnNone.setAttribute('id', 'btnNone' + j);
            btnNone.setAttribute('href', '#');
            btnNone.textContent = 'None';
            btnNone.className = 'left button';
            var btnFew = document.createElement('a');
            btnFew.setAttribute('id', 'btnFew' + j);
            btnFew.setAttribute('href', '#');
            btnFew.textContent = 'A few';
            btnFew.className = 'middle button';
            var btnNormal = document.createElement('a');
            btnNormal.setAttribute('id', 'btnNormal' + j);
            btnNormal.setAttribute('href', '#');
            btnNormal.textContent = 'Some';
            btnNormal.className = 'middle button';
            var btnLot = document.createElement('a');
            btnLot.setAttribute('id', 'btnLot' + j);
            btnLot.setAttribute('href', '#');
            btnLot.textContent = 'A lot';
            btnLot.className = 'right button';


            li.appendChild(span);
            div.appendChild(btnNone);
            div.appendChild(btnFew);
            div.appendChild(btnNormal);
            div.appendChild(btnLot);
            li.appendChild(div);
            el.appendChild(li);

            setTextureUsage(j, 'None');
        }

        setTextureUsage(0, 'Lot');
        generate();


        function setTextureUsage(id, mode) {
            var modes = ['None', 'Few', 'Normal', 'Lot'];
            var weights = { 'None': 0, 'Few': .3, 'Normal': .7, 'Lot': 1 };
            for (var j = 0; j < modes.length; j++) {
                var el = document.getElementById('btn' + modes[j] + id);
                el.className = el.className.replace(' active', '');
                if (modes[j] == mode) {
                    el.className += ' active';
                    textures[id].weight = weights[mode];
                }
            }
        }




        function createCloud() {

            var div = document.createElement('div');
            div.className = 'cloudBase';
            var x = 256 - (Math.random() * 512);
            var y = 256 - (Math.random() * 512);
            var z = 256 - (Math.random() * 512);
            var t = 'translateX( ' + x + 'px ) translateY( ' + y + 'px ) translateZ( ' + z + 'px )';
            div.style.webkitTransform =
            div.style.MozTransform =
            div.style.oTransform =
            div.style.transform = t;
            world.appendChild(div);

            for (var j = 0; j < 5 + Math.round(Math.random() * 10) ; j++) {
                var cloud = document.createElement('img');
                cloud.style.opacity = 0;
                var r = Math.random();
                var src = 'troll.png';
                for (var k = 0; k < computedWeights.length; k++) {
                    if (r >= computedWeights[k].min && r <= computedWeights[k].max) {
                        (function (img) {
                            img.addEventListener('load', function () {
                                img.style.opacity = .8;
                            })
                        })(cloud);
                        src = computedWeights[k].src;
                    }
                }
                if (!isKosher) src = 'troll.png';
                cloud.setAttribute('src', src);
                cloud.className = 'cloudLayer';

                var x = 256 - (Math.random() * 512);
                var y = 256 - (Math.random() * 512);
                var z = 100 - (Math.random() * 200);
                var a = Math.random() * 360;
                var s = .25 + Math.random();
                x *= .2; y *= .2;
                cloud.data = {
                    x: x,
                    y: y,
                    z: z,
                    a: a,
                    s: s,
                    speed: .1 * Math.random()
                };
                var t = 'translateX( ' + x + 'px ) translateY( ' + y + 'px ) translateZ( ' + z + 'px ) rotateZ( ' + a + 'deg ) scale( ' + s + ' )';
                cloud.style.webkitTransform =
                cloud.style.MozTransform =
                cloud.style.oTransform =
                cloud.style.transform = t;

                div.appendChild(cloud);
                layers.push(cloud);
            }

            return div;
        }

        window.addEventListener('mousewheel', onContainerMouseWheel);
        window.addEventListener('DOMMouseScroll', onContainerMouseWheel);



        window.addEventListener('keydown', function (e) {
            if (e.keyCode == 32) generate();
        });

        window.addEventListener('mousemove', function (e) {
            worldYAngle = -(.5 - (e.clientX / window.innerWidth)) * 180;
            worldXAngle = (.5 - (e.clientY / window.innerHeight)) * 180;
            //worldXAngle = .1 * ( e.clientY - .5 * window.innerHeight );
            //worldYAngle = .1 * ( e.clientX - .5 * window.innerWidth );
            updateView();
        });

        window.addEventListener('touchmove', function (e) {
            var ptr = e.changedTouches.length;
            while (ptr--) {
                var touch = e.changedTouches[ptr];
                worldYAngle = -(.5 - (touch.pageX / window.innerWidth)) * 180;
                worldXAngle = (.5 - (touch.pageY / window.innerHeight)) * 180;
                updateView();
            }
            e.preventDefault();
        });

        function generate() {
            objects = [];
            if (world.hasChildNodes()) {
                while (world.childNodes.length >= 1) {
                    world.removeChild(world.firstChild);
                }
            }
            computedWeights = [];
            var total = 0;
            for (var j = 0; j < textures.length; j++) {
                if (textures[j].weight > 0) {
                    total += textures[j].weight;
                }
            }
            var accum = 0;
            for (var j = 0; j < textures.length; j++) {
                if (textures[j].weight > 0) {
                    var w = textures[j].weight / total;
                    computedWeights.push({
                        src: textures[j].file,
                        min: accum,
                        max: accum + w
                    });
                    accum += w;
                }
            }
            for (var j = 0; j < 5; j++) {
                objects.push(createCloud());
            }
        }

        function updateView() {
            var t = 'translateZ( ' + d + 'px ) rotateX( ' + worldXAngle + 'deg) rotateY( ' + worldYAngle + 'deg)';
            world.style.webkitTransform =
            world.style.MozTransform =
            world.style.oTransform =
            world.style.transform = t;
        }

        function onContainerMouseWheel(event) {

            event = event ? event : window.event;
            d = d - (event.detail ? event.detail * -5 : event.wheelDelta / 8);
            updateView();

        }

        function orientationhandler(e) {

            if (!e.gamma && !e.beta) {
                e.gamma = -(e.x * (180 / Math.PI));
                e.beta = -(e.y * (180 / Math.PI));
            }

            var x = e.gamma;
            var y = e.beta;

            worldXAngle = y;
            worldYAngle = x;
            updateView();

        }

        function update() {

            for (var j = 0; j < layers.length; j++) {
                var layer = layers[j];
                layer.data.a += layer.data.speed;
                var t = 'translateX( ' + layer.data.x + 'px ) translateY( ' + layer.data.y + 'px ) translateZ( ' + layer.data.z + 'px ) rotateY( ' + (-worldYAngle) + 'deg ) rotateX( ' + (-worldXAngle) + 'deg ) rotateZ( ' + layer.data.a + 'deg ) scale( ' + layer.data.s + ')';
                layer.style.webkitTransform =
                layer.style.MozTransform =
                layer.style.oTransform =
                layer.style.transform = t;
                //layer.style.webkitFilter = 'blur(5px)';
            }

            requestAnimationFrame(update);

        }

        update();

	</script>


</body>
</html>
