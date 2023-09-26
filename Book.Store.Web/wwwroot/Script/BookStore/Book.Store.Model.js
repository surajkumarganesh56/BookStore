function BookModel(item) {
    var self = this;
    item = item || {};
    self.id = ko.observable(item.id || 0);
    self.title = ko.observable(item.title || '');
    self.author = ko.observable(item.author || '');
    self.genre = ko.observable(item.genre || '');
    self.category = ko.observable(item.category || '');
    self.quantity = ko.observable(item.quantity || 0);
    self.price = ko.observable(item.price || 0.00);
    self.createdDate = ko.observable(item.createdDate || 0);
}

function FilterModel(item) {
    var self = this;
    item = item || {};
    self.genre = ko.observable(item.genre || '');
    self.author = ko.observable(item.author || '');
}