var webPath = window.webPath;

if (typeof moment === "function") {
    moment.locale("zh-cn");
}
function getUrlParam(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}
function formDate(dateStr) {
    if (dateStr != "") {
        return new Date(dateStr);
    }
}
function block() {
    var _block = appendHtmlFile("#_block_", "/template/block.html");
    _block.show();
}

function unblock() {
    $("#_block_").hide();
}

function loadHtmlFile(url) {
    var html = "";
    url = $url(url);
    jQuery.ajax({
        url: url, type: "GET", async: false, success: function (data) {
            html = data;
        }
    });
    return html;
}

function appendHtmlFile(ele, url) {
    if (typeof (ele) == "string") {
        ele = $(ele);
    }
    if (ele.length == 0) {
        ele = $(loadHtmlFile(url));
        /* adjustDialog
        ele.on("shown.bs.modal", function (event) {
            var e = $(event.target).find(".modal-dialog");
            var a = $(event.target).height();
            var b = e.height();
            e.css("top", ((a - b)/2 / a)*100 + "%");             
        });
        */
        $(document.body).append(ele);
    }
    return ele;
}

function $url(url) {
    if (!webPath) {
        alert("webPath:" + webPath);
    }
    if (url.indexOf("http:") != 0 && url.indexOf("https:") != 0) {
        var reg = new RegExp(webPath + "/", "ig");
        url = url.replace(reg, "/");
        url = (webPath + "/" + url).replace(/:\/\//ig, "~").replace(/[\/]+/ig, "/").replace(/~/ig, "://");
    }
    return url;
}
/**
		*
		提示信息
		*		
		@method 
		*
		@param {string} msg		要显示的文字内容	
		@param {string} title	弹出框的标题
		*
		@returns {object}
 */
function $Alert(msg, title) {
    msg = msg || "";
    var that = {};
    unblock();
    var _error = appendHtmlFile($("#__alert"), "/template/alert.html");
    _error.off("hidden.bs.modal");
    _error.on("hidden.bs.modal", function () {
        if (typeof that.func === "function") {
            that.func();
        }
    });

    _error.modal({ show: true, backdrop: "static" });

    if (msg instanceof Element) {
        _error.find(".modal-body").html("").append(msg);
    } else {
        if (msg && msg.indexOf("<html") > -1) {
            _error.find(".modal-body").css("padding", "0 15px");
            var doc = _error.find(".modal-body #content").contents();
            if (doc.length > 0) {
                doc[0].open();
                doc[0].write(msg);
                doc[0].close();
                try {
                    doc[0].window.resize();
                    doc[0].body.style.color = "#4465a2";
                } catch (e) { }
            }
        } else {
            msg = msg.replace(/[\r\n]/ig, "<br/>");
            var doc = _error.find(".modal-body").html((msg || ""));
        }
    }

    if (title) {
        if (typeof title == "string") {
            _error.find(".modal-title").text(title);
        }
    }
	/**
		*
		关闭显示框后的其他操作
		*		
		@method then
		*
		@for $Alert
		*
		@param {function} func		关闭显示框后要执行的函数
		*
		@returns {object}
 */
    that.then = function (func) {
        that.func = func;
    };
    return that;
}
function $AlertUrl(url, title) {
    url = url || "";
    var that = {};
    unblock();
    var _error = appendHtmlFile($("#__alert"), "/template/alertPage.html?t=" + new Date());
    _error.off("hidden.bs.modal");
    _error.on("hidden.bs.modal", function () {
        if (typeof that.func === "function") {
            that.func();
        }
    });

    _error.modal({ show: true, backdrop: "static" });
    _error.find("#content")[0].src = url;
	/**
		*
		关闭显示框后的其他操作
		*		
		@method then
		*
		@for $Alert
		*
		@param {function} func		关闭显示框后要执行的函数
		*
		@returns {object}
 */
    that.then = function (func) {
        that.func = func;
    };
    return that;
}
/**
		*
		显示确认对话框
		*		
		@method $Confirm
		*
		@param {string} msg		要显示的文字内容
		@param {function} func	点击确认后要执行的方法
		@param {string} title	弹出框的标题
		*
		@returns {void} 无返回值
 */
function $Confirm(msg, func, title) {
    var that = {};
    unblock();
    var _confirm = appendHtmlFile($("#__confirm"), "/template/confirm.html?t=" + new Date());
    title = title || "确认操作";
    _confirm.find(".modal-title").text(title);
    _confirm.modal({ show: true, backdrop: "static" });
    var doc = _confirm.find(".modal-body").html(msg);
    _confirm.off("hidden.bs.modal");
    _confirm.on("hidden.bs.modal", function () {
        if (typeof that.func === "function") {
            that.func();
        }
    });
    _confirm.find("#confirm-btn").one("click", function () {
        _confirm.modal("hide");
        that.func = func;
    });
}
window.autoIframe = autoIframe;
function autoIframe(obj, ele) {
    return;
    ele = $(obj).parents(".modal-dialog");
    var w = $(obj).contents().width();
    var h = $(obj).contents().height();
    if (w > $(window).width()) {
        w = $(window).width();
    }
    if (h > $(window).height()) {
        h = $(window).height();
    }
    if (w > 900) { w = 900; }
    if (w < 400) { w = 400; }
    if (h > 550) { h = 550; }
    if (h < 300) { h = 300; }
    $(ele).width(w + 20).height(h);
    $(obj).width(w).height(h > 200 ? h - 150 : h);
    var m_top = ($(window).height() - $(ele).height()) / 2;
    $(ele).css({ 'margin': (m_top - 34) + 'px auto' });
    window.onresize = function () { autoIframe(obj, ele); };
}

function queryService($http, $q, $timeout, $sce) {
    /**
	  ajax only use angualr's $http
	 *
	 * @param {string} url
	 * @param {object|function} data
	 * @param {function} functions
		@returns {void}
	 */
    this.put = function (url, model, success) {
        return http("PUT", url, model, success);
    };
    this.get = function (url, model, success) {
        return http("GET", url, model, success);
    };
    this.delete = function (url, model, success) {
        return http("DELETE", url, model, success);
    };
    this.post = function (url, model, success) {
        return http("POST", url, model, success);
    };
    this.head = function (url, model, success) {
        return http("HEAD", url, model, success);
    }
    this.jsonp = function (url, model, success) {
        return http("JSONP", url, model, success);
    }
    this.patch = function (url, model, success) {
        return http("PATCH", url, model, success);
    }
    function http(method, url, model, success) {
        url = $url(url);
        block();
        var data = model;
        var _func = null;
        if (typeof (model) == "function") {
            data = null;
            _func = model;
        } else {
            _func = success;
        }
        var req = {
            method: method,
            url: url,
            headers: {
                'Content-Type': undefined,
                'X-Requested-With': 'JSON'
            },
            data: data,
        };
        switch (method) {
            case "GET":
                req.params = data;
                //req.headers["Content-Type"] = "application/x-www-form-urlencoded";
                break;
            case "PUT":
            case "POST":
                req.headers["Content-Type"] = "application/json; charset=utf-8";
                //req.headers["Access-Control-Allow-Methods"]="POST,GET,PUT";
                //req.headers["Access-Control-Allow-Headers"]="X-Requested-With,Content-Type";
                break;
        }
        var deferred = $q.defer(); // 声明延后执行，表示要去监控后面的执行  
        $http(req).then(function (response) {
            var _data = getReponseData(response);
            console.log(url, data, _data);
            unblock();
            try {
                if (_data.status) {
                    if (_data.status !== 200) {
                        _Error(_data, url, _func);
                        deferred.reject(_data);   // 声明执行失败，即服务器返回错误  
                        return;
                    }
                }
                var $data = _func(_data);
                if (!$data) {
                    deferred.resolve(_data);  // 声明执行成功，即http请求数据成功，可以返回数据了  
                } else {
                    deferred.resolve($data);  // 声明执行成功，即http请求数据成功，可以返回数据了  
                }

            } catch (e) {
                console.info(e);
                $Alert(e.message);
            }
        }, function (response) {
            var _data = getReponseData(response);
            console.info("Error", _data);
            unblock();
            _Error(_data, url);
            deferred.reject(_data);   // 声明执行失败，即服务器返回错误  
        });
        return deferred.promise;   // 返回承诺，这里并不是最终数据，而是访问最终数据的API  
    }
    function getReponseData(response) {
        var data = { status: response.status, message: response.statusText };
        if (response.data) {
            if ((typeof response.data) == "string") {
                data.message = response.data;
            } else {
                data = response.data;
                if (data && !data.message) {
                    data.message = response.statusText;
                }
            }
        }
        if (!data.message || data.message.length == 0) {
            data.message = "response 未知错误!";
        }
        return data;
    }

    function _Error(data, url, func) {
        unblock();
        var msg = data.message;
        if (!msg) {
            var status = data.status;
            switch (status) {
                case 404:
                    msg = "请求的资源不存在: " + url;
                    break;
                case 401:
                    //msg = "Unauthorized : Please log in first.";
                    $Alert("请登录!").then(function () {
                        top.location.href = window.webPath;
                    });
                    return;
                /* case 500:
                     msg = "Internal Server Error";
                     break;
                 case 502:
                     msg = "Bad Gateway";
                     break;
                 case 503:
                     msg = "Service Unavailable";
                     break;
                     */
            }
        }
        $Alert(msg);
        if (func) { func(data); }
    }
    return this;
}

var $angular = {
	/**
		*
		将要执行的函数放到集合,以便 run时再统一执行 $scope, $query, $timeout, $sce
		*
		@method add
		*
		@for MyFunction
		*
		@param {function} func $scope, $query, $timeout, $sce 要执行的函数
		@param {number} index func要执行的顺序, 可为null/undefind
			如果index冲突 则续延
			如果未赋值 则从0开始,如果0已使用,则使用1 ..... 以此类推
		*
		@returns {void} 无返回值
 */

    add: function (func, index) {
        if (this.funcList == null) { this.funcList = []; }
        if (this.funcIndex == null) { this.funcIndex = []; }
        var _index = index || this.funcList.length;
        if (index == undefined) {
            for (var i = 0; i < this.funcIndex.length; i++) {
                if (!this.funcIndex[i]) {
                    _index = i;
                    break;
                }
            }
        } else {
            if (this.funcIndex[index]) {
                _index = this.funcIndex.length;
            }
        }
        this.funcIndex[_index] = true;
        this.funcList[_index] = func;
    },
	/**
		*
		将集合中的函数统一执行
		*
		@method run
		*
		@for MyFunction
		*
		@param {Object[]} args 要执行的参数
		*
		@returns {void} 无返回值
 */
    run: function ($this) {
        for (var f in this.funcList) {
            var func = this.funcList[f];
            if (typeof func == "function") {
                try {
                    func.apply($this, arguments);
                } catch (e) { console.error(e); }
            }
        }
    }
};
var angularApp = angular.module('angularApp', ['ui.bootstrap', 'ng.ueditor', 'ngSanitize', 'ngCsv']);
//angularApp.config(['$locationProvider', function ($locationProvider) {
//    // $locationProvider.html5Mode(true);  
//    $locationProvider.html5Mode({
//        enabled: true,
//        requireBase: false
//    });
//}]);
//angularApp.directive("ngModel", angularValid);
//angularApp.directive("ngModel", optimizSelect);
var angularCtrl = function ($scope, $query, $timeout, $sce, $compile, $interval) {
    $scope.$on('handleErrorOcurs', function (scope, result) {
        console.log("handleErrorOcurs", result);
    });
    $scope.getHtml = function (content) { return $sce.trustAsHtml(content); }
    try {
        $angular.run($scope, $query, $timeout, $sce, $compile, $interval);
        $timeout(function () {
            //$(".wait-loaded").css("visibility", "visible");
            $(".wait-loading").removeClass("wait-loading");
            $(".wait-loaded").removeClass("wait-loaded");
        });
    } catch (e) {
        console.error(e);
    }
    //$timeout(function () {
    // 	initDateTimePicker();
    //});
};

angularApp.service('$query', ["$http", "$q", "$timeout", "$sce", queryService])
    .controller("angularCtrl", ["$scope", "$query", "$timeout", "$sce", "$compile", "$interval", angularCtrl]);

function $moment(t) {
    moment.locale("zh-cn");
    return moment(new Date(t));
}
var _dateOption = { format: "YYYY-MM-DD", showTodayButton: true, useCurrent: false, keepInvalid: true, locale: "zh-cn" };
var _dateTimeOption = { format: "YYYY-MM-DD HH:mm", useCurrent: false, sideBySide: true, keepInvalid: true, locale: "zh-cn" };
angularApp.directive("mydatetimepicker", ["$compile", function ($compile) {
    return {
        replace: false,
        restrict: 'C',
        link: function (scope, element, attrs, transclude) {
            var options = scope.options;
            if (!scope.options) {
                if (scope.showTime) {
                    options = _dateTimeOption;
                } else {
                    options = _dateOption;
                }
            }
            options.widgetParent = element.parent();
            element.datetimepicker(options);
            var val = $moment(scope.ngModel);
            if (val._isValid) {
                scope.ngModel = val.format(options.format);
                element.val(scope.ngModel);
            }

            element.on("keydown", function (e) {
                if (e.keyCode == 8 || e.keyCode == 46) {
                    this.value = scope.ngModel = "";
                    return true;
                }
                return false;
            });
            var picker = element.data().DateTimePicker;
            if (scope.minDate) {
                picker.minDate(scope.minDate);
            }
            if (scope.maxDate) {
                picker.maxDate(scope.maxDate);
            }
            $('<i class="glyphicon glyphicon-calendar"></i>').insertAfter(element);
            scope.$parent.$watch(attrs.ngMinDate, function (a, b, c) {
                if (scope.minDate) {
                    try {
                        picker.maxDate(false);
                        picker.minDate($moment(scope.minDate));
                        if (scope.maxDate) {
                            picker.maxDate($moment(scope.maxDate));
                        }
                    } catch (e) { console.error(e, scope); }
                }
                return a;
            });
            scope.$parent.$watch(attrs.ngMaxDate, function (a, b, c) {
                if (scope.maxDate) {
                    picker.maxDate(false);
                    if (scope.minDate) {
                        picker.minDate($moment(scope.minDate));
                    }
                    picker.maxDate($moment(scope.maxDate));
                }
                return a;
            });
            scope.$parent.$watch(attrs.ngModel, function (a, b, c) {
                if (scope.ngModel) {
                    var val = $moment(a);
                    if (val._isValid) {
                        a = val.format(options.format);
                        element.val(a);
                    }
                }
                return a;
            });
        },
        scope: { ngModel: "=", minDate: "=ngMinDate", maxDate: "=ngMaxDate", options: "=ngDateOptions", showTime: "=ngShowTime" },
        require: 'ngModel'
    };
}]);

(function ($) {
    $.fn.showImage = function (zIndex) {
        return $.showImage(this, zIndex);
    };
    $.showImage = function (target, zIndex) {
        if (!zIndex) {
            zIndex = 1051;
        } else {
            zIndex = zIndex * 1;
            if (zIndex < 1051) {
                zIndex = 1051;
            }
        }
        var _modal = $('<div class="modal fade bs-example-modal-lg text-center" id="__imgModal" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel"><div class="modal-dialog modal-lg" style="display: inline-block; width: auto;"><div class="modal-content"><img id="__imgInModal" src=""></div></div></div>');
        target.find("img").on("click", function (e) {
            e.stopPropagation();
            e.preventDefault();
            if ($("#__imgModal").length == 0) {
                $(document.body).append(_modal);
            } else {
                _modal = $("#__imgModal");
            }
            $("#__imgModal #__imgInModal").attr("src", this.src);
            _modal.on("showing.bs.modal", function (e) {
                var _this = $(e.delegateTarget);
                $(e.backdrop).css("z-index", zIndex);
                _this.css("z-index", zIndex + 10);
            }).modal("show");
        });
        return target;
    };
})(jQuery);