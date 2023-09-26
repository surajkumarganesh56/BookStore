/// <reference path="../riddha.globals.ko.js" />
function menuActionController() {
    var self = this;
    self.Menus = ko.observableArray([]);
    self.ActionCodes = ko.observableArray([]);
    self.SearchField = ko.observable('check');


    function getMenus() {
        Riddha.ajax.get("/Api/MenuApi/GetMenus")
            .done(function (result) {
                var data = Riddha.ko.global.arrayMap(result.data, MenuModel);
                self.Menus(data);
            })
    } getMenus();

}