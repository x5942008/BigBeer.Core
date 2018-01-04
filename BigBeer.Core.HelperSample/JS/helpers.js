//扩展简单的ajax处理
$.extend({
    sajax: function (op) {
        return $.ajax({ url: op.url, type: "post", data: op.data, dataType: "json" });
    },
    //获取所有选中的checkbox
    check: function () {
        var id = [];
        var $els = $("td input:checkbox");
        $els.each(function () {
            if ($(this).is(":checked"))
                id.push($(this).attr("id"));
        });
        return {
            id: id,
            el: $els,
            //是否有值
            hasValue: id.length>0
        };
    },
    //显示模态窗口
    modal: function (op) {
        var $el = {};
        if (!op) op = {};
        var options = {
            url: op.url ?  op.url:"/Template/modal.html" ,
            title:op.title,
            ok: op.ok ? op.ok : function (r,p) {
                r.close();
            },
            close: op.close ? op.close : function () {
                $el.remove();
            },
            message:op.msg
        };
        var isshow = false;
        _this = this;
        var result = {
            close: function () {
                if ($el.remove)
                {
                    $el.remove();
                    clearEvent();
                }
                return _this;
            },
            show: function () {
                if ($el.show) {
                    $el.show();
                    
                }
                else
                    isshow = true;
                return _this;
            }
        };
        var clearEvent = function () {
            $("body").undelegate(".modal [modal-ok]", "click");
            $("body").undelegate(".modal [modal-close]", "click");
        }
        $("body").delegate(".modal [modal-close]", {
            click: function () {
                options.close();
                clearEvent();
            }
        });
        $("body").delegate(".modal [modal-ok]", {
            click: function () {
                var $im = $(".modal .modal-body").find("input,select,textarea");
                var params = [];
                var pstring = "{";
                $im.each(function (i) {
                    var key = $(this).attr("id");
                    var value = $(this).val();
                    pstring +="\""+ key + "\":\"" + value+"\"";
                    if(i<$im.length-1)
                        pstring+=","
                });
                pstring += "}";
                params = JSON.parse(pstring);
                options.ok(result, params);
            }
        });
        $.get(options.url, function (r) {
            $el = $(r);
            $("body").append($el);
            if (options.message)
                $("[modal-body]").html(options.message);
            if (options.title)
                $("[modal-title]").html(options.title);
            if (isshow)
                $el.show();
        });
        
        return result;
    }
});
//扩展容器的ajax获取数据并利用模板渲染
$.fn.extend({
    tempalte: function (temp, data, clear) {
        if (clear)
            $(this).empty();
        $(this).append(template(temp, data));
    },
    //url:访问api 地址,data 回传给api的数据, temp 模板id,clear 是否先清空
    ajax: function (url, data, temp, clear) {
        var _this = $(this);
        return $.sajax({ url: url, data: data })
         .done(function (r) {
             _this.tempalte(temp, r.d, clear);
         });
    }
});
//全选反选
$(function () {
    $("#box").delegate("th input:checkbox", {
        click: function () {
            $("td input:checkbox").attr("checked", $(this).is(":checked"));
        }
    });
});
