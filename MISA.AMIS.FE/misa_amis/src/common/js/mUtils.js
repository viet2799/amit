import moment from 'moment'

export const func = {
    functionName: (data) => {
        console.log(data);
    },
    /**
     * TODO:Format tiền tệ
     * CreateBy:ntquan(21/04/2021)
     */
    fnFormatCurrency: function (num) {
        if (typeof num == "string") {
            while (num.indexOf(",") != -1) num = num.replace(",", "");
        }
        num = parseInt(num);
        num += "";

        var p = num.split(".");
        // acc: tích luỹ cộng dồn
        return p[0]
            .split("")
            .reverse()
            .reduce(function (acc, num, i) {
                return num == "-" ? acc : num + (i && !(i % 3) ? "," : "") + acc;
            }, "");
    },

    fnFormatCurrencyV1: function (nStr, subLength) {
        nStr += "";
        //console.log(typeof nStr)
        //if(nStr.indexOf('.')!=-1) nStr=nStr.replace(".","")

        var x = nStr.split(".");
        var x1 = x[0];
        var x2 = "";
        if (subLength > 0)
            x2 = x.length > 1 ? "," + x[1].substring(0, subLength) : "";
        var rgx = /(\d+)(\d{3})/;
        while (rgx.test(x1)) {
            x1 = x1.replace(rgx, "$1" + "." + "$2");
        }
        return x1 + x2;
    },
    /**
     * TODO:Clone new obj
     * @param {any} obj
     * CreateBy:ntquan(21/04/2021)
     */
    cloneObject: function (obj) {
        var arr = [];
        var listKey = [];
        var listValue = [];
        listKey = Object.keys(obj);
        listValue = Object.values(obj);
        for (var i = 0; i < listKey.length; i++) {
            arr.push({ key: listKey[i], value: listValue[i] });
        }

        var result = arr.reduce(function (map, obj) {
            map[obj.key] = obj.value;
            return map;
        }, {});
        return result;
    },
    /**
     * TODO:Set null cho tất cả thuộc tính của object
     * CreateBy:ntquan(21/04/2021)
     */
    setNull: function (obj, val) {
        Object.keys(obj).forEach(function (index) {
            obj[index] = val;
        });
    },

    /**
     * TODO:Format DateTime
     * CreateBy:ntquan(21/04/2021)
     */
    fnFormatDate: function (dateInput) {
        return moment(String(dateInput)).format('DD/MM/YYYY')
    },

    /**
     * TODO:Format date bind vào input
     * CreateBy:ntquan(21/04/2021)
     */
    fnFormatDateInput: function (dateInput) {
        return moment(dateInput).format('YYYY-MM-DD')
    },

    /**
     * TODO:Format date to ISO
     * CreateBy:ntquan(21/04/2021)
     */
    fnFormatISO: function (dateInput) {
        var date = new Date(dateInput);
        return date.toISOString();
    },

    /**
     * TODO:Query search
     * CreateBy:ntquan(21/04/2021)
     */
    fnBuildQueryString: function (query) {
        const params = new URLSearchParams(query);
        return params.toString();
    },

    /**
     * TODO:Format currency to int
     * CreateBy:ntquan(24/04/2021)
     */
    fnFormatCurrencyToInt: function (value) {
        if (typeof value == "string") {
            while (value.indexOf(",") != -1) value = value.replace(",", "");
            return value;
        }
    },
    /**
     * TODO:Validate emaii
     * CreateBy:ntquan(24/04/2021)
     */
    validateEmail: function (email) {
        var re = /\S+@\S+\.\S+/;
        return re.test(email);
    },

    /**
     * TODO:Display alert confirm
     * CreateBy:ntquan(24/04/2021)
     */
    confirmAlert: function (point, callback) {
        var me = point;
        me.$swal
            .fire({
                title: "Bạn có chắc chắn?",
                text: "Bạn có muốn xoá dữ liệu này?",
                icon: "warning",
                showCancelButton: true,
                cancelButtonText: "Huỷ bỏ",
                confirmButtonColor: "#3085d6",
                cancelButtonColor: "#d33",
                confirmButtonText: "Đồng ý",
            })
            .then((result) => {
                if (result.isConfirmed) {
                    if (typeof callback == "function") callback();
                }
            });
    },

    /**
     * TODO:Display alert state of action
     * CreateBy:ntquan(24/04/2021)
     */
    stateAlert(point, title, msg, type) {
        var me = point;
        me.$swal.fire(title, msg, type);
    },
    /**
     *
     */
    fnSortTable() {
        const getCellValue = (tr, idx) =>
            tr.children[idx].innerText || tr.children[idx].textContent;

        const comparer = (idx, asc) => (a, b) =>
            ((v1, v2) =>
                v1 !== "" && v2 !== "" && !isNaN(v1) && !isNaN(v2)
                    ? v1 - v2
                    : v1.toString().localeCompare(v2))(
                        getCellValue(asc ? a : b, idx),
                        getCellValue(asc ? b : a, idx)
                    );

        // do the work...
        document.querySelectorAll("th").forEach((th) =>
            th.addEventListener("click", () => {
                const table = th.closest("table");
                Array.from(table.querySelectorAll("tr:nth-child(n+2)"))
                    .sort(
                        comparer(
                            Array.from(th.parentNode.children).indexOf(th),
                            (this.asc = !this.asc)
                        )
                    )
                    .forEach((tr) => table.appendChild(tr));
            })
        );
    },

    fnResizeable() {
        var element = document.getElementByClass("dialog-content");
        //create box in bottom-left
        var resizer = document.createElement("div");
        resizer.style.width = "10px";
        resizer.style.height = "10px";
        resizer.style.background = "red";
        resizer.style.position = "absolute";
        resizer.style.right = 0;
        resizer.style.bottom = 0;
        resizer.style.cursor = "se-resize";
        //Append Child to Element
        element.appendChild(resizer);
        //box function onmousemove
        resizer.addEventListener("mousedown", initResize, false);

        //Window funtion mousemove & mouseup
        function initResize() {
            window.addEventListener("mousemove", Resize, false);
            window.addEventListener("mouseup", stopResize, false);
        }
        //resize the element
        function Resize(e) {
            element.style.width = e.clientX - element.offsetLeft + "px";
            element.style.height = e.clientY - element.offsetTop + "px";
        }
        //on mouseup remove windows functions mousemove & mouseup
        function stopResize() {
            window.removeEventListener("mousemove", Resize, false);
            window.removeEventListener("mouseup", stopResize, false);
        }
    },

    /**
     *
     */
    move: function (divid, xpos, ypos) {
        divid.style.left = xpos + "px";
        divid.style.top = ypos + "px";
    },
    startMoving: function (divid, wrapper, evt) {
        evt = evt || window.event;
        var rect = divid.getBoundingClientRect();
        var posX = evt.clientX,
            posY = evt.clientY,
            divTop = rect.top,
            divLeft = rect.left,
            //width height div
            eWi = parseInt(divid.style.width),
            eHe = parseInt(divid.style.height),
            //width height wrapper
            cWi = parseInt(document.getElementById(wrapper).style.width),
            cHe = parseInt(document.getElementById(wrapper).style.height);
        //width cursor to modal
        var diffX = posX - divLeft,
            diffY = posY - divTop;
        //chỉ cho phép drag ở cùng chỉ định
        if (diffY > 50) {
            return;
        }
        divid.onmousemove = function (evt) {
            evt = evt || window.event;
            var posX = evt.clientX,
                posY = evt.clientY,
                aX = posX - diffX,
                aY = posY - diffY;
            if (aY < -50) this.stopMoving(divid);
            if (aX < 0) aX = 0;
            if (aY < 0) aY = 0;
            if (aX + eWi > cWi) aX = cWi - eWi;
            if (aY + eHe > cHe) aY = cHe - eHe;
            func.move(divid, aX, aY);
        };
    },
    stopMoving: function (divid) {
        divid.onmousemove = function () { };
    },

    /**
     *
     */
    not: function () {
        var matchedElements = document.querySelectorAll("tr td:not(.selected)");
        console.log(matchedElements);
    },
    excludeByClassName: function (className) {
        return function (element) {
            return element.className == className;
        };
    },
};
