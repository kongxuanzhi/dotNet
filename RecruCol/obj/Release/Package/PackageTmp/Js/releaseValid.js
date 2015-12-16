//function CheckTitle() {
//    var title = document.getElementById("infoTitle");
//    var titleW = document.getElementById("titleW");
//    if (title.value == "" || title.value == null) {
//        titleW.innerHTML = "<img src='../images/error.png' />请输入文章标题";
//        titleW.style = "color:red";
//        return false;
//    }
//    else {
//        titleW.innerHTML = "<img src='../images/ok.png'/>"
//        return true;
//    }
//}

function CheckCount() {
    var ManCount = document.getElementById("ManCount");
    var ManCountW = document.getElementById("ManCountW");
    if (ManCount.value == "" || ManCount.value == null) {
        ManCountW.innerHTML = "<img src='../images/error.png' />请输入要求人数";
        ManCountW.style = "color:red";
        return false;
    }
    else {
        ManCountW.innerHTML = "<img src='../images/ok.png'/>";
        return true;

    }
}
function CheckPos() {
    var pos = document.getElementById("pos");
    var posW = document.getElementById("posW");
    if (pos.value == "" || pos.value == null) {
        posW.innerHTML = "<img src='../images/error.png' />请输入职位名称";
        posW.style = "color:red";
        return false;
    }
    else {
        posW.innerHTML = "<img src='../images/ok.png'/>";
        return true;
    }
}


function CheckArttype() {
    var arttype = document.getElementById("arttype");
    var arttypeW = document.getElementById("arttypeW");
    if (arttype.value == -1) {
        arttypeW.innerHTML = "<img src='../images/error.png' />请选择文章类别";
        arttypeW.style = "color:red";
        return false;
    }
    else {
        arttypeW.innerHTML = "<img src='../images/ok.png'/>";
        return true;
    }
} 
function CheckGender() {
    var gender = document.getElementById("gender");
    var genderW = document.getElementById("genderW");
    if (gender.value == -1) {
        genderW.innerHTML = "<img src='../images/error.png' />请选择性别要求";
        genderW.style = "color:red";
        return false;
    }
    else {
        genderW.innerHTML = "<img src='../images/ok.png'/>"
        return true;
    }
}
function CheckEduRequir() {
    var EduRequir = document.getElementById("EduRequir");
    var EduRequirW = document.getElementById("EduRequirW");
    if (EduRequir.value == -1) {
        EduRequirW.innerHTML = "<img src='../images/error.png' />请选择学历要求";
        EduRequirW.style = "color:red";
        return false;
    }
    else {
        EduRequirW.innerHTML = "<img src='../images/ok.png'/>"
        return true;
    }
}


function CheckAll()
{
    var sub = document.getElementById("sub");
    var flag = 1;
    if (!CheckPos())
    {
        flag = 0;
    }
    if(!CheckArttype())
    {
        flag = 0;
    }
    if (!CheckCount()) {
        flag = 0;
    }
    if (!CheckGender()) {
        flag = 0;
    }
    if (!CheckEduRequir())
    {
        flag = 0;
    }
    if (flag == 1) {
        return true;
    }
    else
        return false;
}
