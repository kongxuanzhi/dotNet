
function CheckDelete() {
    var allCheck = document.getElementById("Totalselect");
    var check = document.getElementsByName("delId");

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

function ajaxDelete(refresh,deletepage)
{
    var ids = "";
    var check = document.getElementsByName("delId");
    for (var i=0;i<check.length;i++)
    {
        if(check[i].checked==true)
        {
            ids += check[i].value + "|";
        }
    }
    if (ids !="")
    {
        jQuery.ajax({
            type: "post",
            dataType: "plain",
            async: false,
            url: deletepage,
            data: { Ids: ids },
            success: function (data) {
            },
        });
        alert("删除成功");
        window.location.href = refresh;
    }
}
