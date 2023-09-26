function MenuModel(item) {
    var self = this;
    item = item || {};
    self.id = ko.observable(item.id || 0);
    self.code = ko.observable(item.code || '');
    self.name = ko.observable(item.name || "");
    self.nameNp = ko.observable(item.nameNp || "");
    self.parentCode = ko.observable(item.parentCode  || '');
    self.isGroup = ko.observable(item.isGroup  || false);
    self.menuUrl = ko.observable(item.menuUrl  || '');
    self.menuIconCss = ko.observable(item.menuIconCss  || '');
    self.children = ko.observableArray(item.menus );
}