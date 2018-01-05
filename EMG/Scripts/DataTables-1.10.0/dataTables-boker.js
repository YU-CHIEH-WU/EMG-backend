
(function ($, undefined) {
    $.fn.detailView = function (options) {
        var self = $(this), hiddenColumn = [], mData = [], totalWidth = 0;
        (new $.fn.dataTable.Api(self.selector)).destroy();

        self.find("thead th").each(function (index) {
            mData.push({
                data: $(this).data("name"),
                className: "td_" + $(this).data("name"),
                width: $(this).data("width") * 1 + "px",
                visible: !$(this).hasClass("hidden").toBool()
            });
            totalWidth += (!$(this).hasClass("hidden")) ? $(this).data("width") * 1 : 0;
        })

        //console.log(mData);

        var defaults = {
            "order": [[0, 'asc']],
            "columnDefs": [{ "visible": false, "targets": hiddenColumn }],
            "columns": mData,
            "searching": true,
            "autoWidth": false,
            "scrollX": true,
            "scrollY": 200,
            "scrollCollapse": false,
            "paging": true,
            "lengthChange": false,
            "deferRender": true,
            "dom": '<"row"<"col-md-12"<"Title">>><"row"<"col-md-12"<"pull-left"f>>><"row"<"col-md-6"<"pull-left"p>><"col-md-6"<"pull-right"<"BtnBlock">>>>rt<"row"<"col-md-6"<"GoPage">><"col-md-6"<"pull-right">>>',
            "pagingType": "full_numbers",
            "draw": 1,
            "processing": false,
            "serverSide": true,
            "ajax": $.fn.dataTable.pipeline({
                "url": self.data("ajaxurl"),
                "pages": 5,
            }),
            "fnServerParams": function (aoData) {
                $("#filter_block select,#filter_block input:text,#filter_block input:hidden").each(function () {
                    aoData[$(this).attr("name")] = $(this).val();
                });
                var sort = "", sortData = "";
                $.each(aoData.order, function (i, obj) {
                    sort += obj.dir + ",";
                    sortData += mData[obj.column].data + ",";
                });
                aoData["Sort"] = sort.substring(0, sort.length - 1);
                aoData["SortData"] = sortData.substring(0, sortData.length - 1);
            },
            "drawCallback": function (settings) {
                var pageInfo = this.api().page.info();
                $("#go_index").val(pageInfo.page + 1);
                $("#total_page").text(pageInfo.pages);
            },
            "pageLength": 15,
            "displayStart": 0,
            "language": {
                "zeroRecords": "找不到符合的資料。",
                "info": "<label>共 _TOTAL_ 頁</label>",
                "search": "查詢：",
                "lengthMenu": "每頁顯示 _MENU_ 筆資料",
                "infoFiltered": "",
                "infoEmpty": "共 0 頁",
                "paginate": {
                    "first": "|◄",
                    "previous": "◄",
                    "next": "►",
                    "last": "►|"
                }
            }
        }

        var settings = $.extend(defaults, options);
        var oTable = self.dataTable(settings);
        self.data("tables", oTable);

        var wrapper = self.closest("div.dataTables_wrapper");
        wrapper.find("div.Title").html(self.data("title"));
        wrapper.find("div.GoPage").html("跳至：<input id='go_index' tabindex='-1' size='5' /> 頁，共<span id='total_page'></span>頁");
        wrapper.find("div.BtnBlock").html($('#BtnBlock'));
        wrapper.find("#DataTable_filter").html($("#filter_block"));

        self.find("*:not(.dataTable th)").off();
        wrapper.find("button.btn").unbind("click");

        var resizeTimer;

        function resizeFunction() {
            var innerWidth = window.innerWidth;
            var height = innerHeight - self.offset().top - 60 + "px";
            var settings = oTable.api().settings();
            settings.context[0].oScroll.sY = height;
            self.parent('.dataTables_scrollBody').css('height', height);
            settings.context[0].oScroll.sXInner = totalWidth + "px";
            settings.context[0].oScroll.sX = "100%";
        };

        $(window).unbind("resize").resize(function () {
            clearTimeout(resizeTimer);
            resizeTimer = setTimeout(resizeFunction, 550);
        });

        resizeFunction();

        $("#go_index").unbind("keydown").keydown(function (e) {
            if (e.which == 13) {
                var index = $(this).val() * 1 - 1;
                if (index >= 0) {
                    oTable.fnPageChange(index);
                }
                else
                    oTable.fnPageChange(0);
            };
        });

        $("#filter-btn").click(function (oSettings) {
            oTable.api().clearPipeline();
            oTable.fnDraw();
        });

        $("#back-btn").click(function () {
            if (opener != null || $("#cancel-btn").data("toClose") || $(".home-link").length == 0) {
                window.close();
            }
            else {
                $(".home-link").get(0).click();
            }
        });

        //$("body").unbind("keydown").keydown(function (e) {
        //    if (e.which == 27) {
        //        if (isChange) {
        //            var $noty = $.openChangeConfirmNoty($this, function () {
        //                window.close();
        //            });
        //        }
        //        else {
        //            window.close();
        //        }
        //    };
        //});

        return oTable;
    }
})(jQuery);