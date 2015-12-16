function CheckDelete() {
    var allCheck = document.getElementById("Totalselect"); //总的checkname
    var check = document.getElementsByName("delId");  //checkname名称

    if (allCheck.checked == false) {
        allCheck.checked = false;
        for (var i = 0; i < check.length; i++) {
            check[i].checked = false;
        }
        return;
    }
    else {
        for (var i = 0; i < check.length; i++) {
            check[i].checked = true;
        }
    }
}

function ajaxDelete(tableName)
{
    alert(tableName);
    var ids = "";
    var check = document.getElementsByName("delId");
    for (var i=0;i<check.length;i++)
    {
        if(check[i].checked==true)
        {
            ids += check[i].value + "|";
        }
    }
    alert(ids);
    if (ids !="")
    {
        alert("dd");
        jQuery("#post").loadUrl("../../operatorSQL/deleteMany.ashx", { Ids: ids, TabName: tableName }, function (data) {
            alert("ds");
        });
    }
    alert("df");
}
