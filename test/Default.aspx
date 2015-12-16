<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <script src="js/jquery-1.8.0.min.js"></script>
    <title></title>
    <link href="css/video-js.css" rel="stylesheet" />
    <script src="js/video.js"></script>
     <script>
         videojs.options.flash.swf = "video-js.swf";
     </script>
    <style>
        .blue
        {
           background-color:blue;
        } 
        li{ cursor: pointer; }       
    </style>
    <script>
        //  jQuery.post(url, data, success(data, textStatus, jqXHR), dataType)
        var videos;
        function getUrls(obj) {
            for (var i = 0; i < videos.length; i++) {
                if (obj.innerHTML == videos[i].Mname) {
                    alert(videos[i].Murl);
                    $("example_video_1 source").src = videos[i].Murl;
                    var myPlayer = videojs('example_video_1').play();
                    //$("#mid").html("xxoo");
                }
            }
        }
        $(function ($) {
            
            
            $("#left ul li").click(function ()
            {
                $("#left ul li").removeClass();
                $(this).addClass("blue");
                $.post("getMovie.ashx", { catalog: this.innerHTML }, function (data) {
                    $("#right").empty();
                    videos = data;
                    if (data != null)
                    {
                        for (var i = 0; i < data.length; i++) {
                            var li = "<li onclick='getUrls(this)'>" + data[i].Mname + "</li>";
                            $("#right").append(li);
                        }
                    }
                },
                "json");
            });
           
        });
      
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="height:800px">
        <div id="left" style="width:25%;height:inherit; float:left;">
            <ul>
                <li ></li>
                <%
                   foreach(string item in catalogs)
                   {
                       Context.Response.Write("<li>"+item+"</li>");    
                   }
                 %>
            </ul>
        </div>
        <div id="mid" style=" background-color:pink;height:inherit; width:50%;float:left">
              <video id="example_video_1" class="video-js vjs-default-skin" controls preload="none" width="640" height="264"
                  poster="http://video-js.zencoder.com/oceans-clip.png"
                  data-setup="{}">
                <source  src="http://video-js.zencoder.com/oceans-clip.mp4" type='video/mp4' />
                </video>
        </div>
        <div id="right" style="width:25%;height:inherit; float:right;background-color:red">
            <ul>
                <li>谭浩强教学视频</li>
                <li>谭浩强教学视频</li>
                <li>谭浩强教学视频</li>
                <li>谭浩强教学视频</li>
                <li>谭浩强教学视频</li>
                <li>谭浩强教学视频</li>
                <li>谭浩强教学视频</li>
            </ul>
        </div>
    </div>
       
    </form>

</body>
</html>
