/// <reference path="book.store.model.js" />

function BookController(id) {
    var self = this;
    var url = "/Api/BookApi";
    self.Book = ko.observable(new BookModel());
    self.Books = ko.observableArray();
    self.skipCount = ko.observable(0);
    self.ModeOfButton = ko.observable("Add");
    self.Filter = ko.observable(new FilterModel());
    self.OpenBookModal = function () {
        $("#BookPopUp").appendTo("body").modal('show');
    }
    self.CloseBookModal = function () {
        $("#BookPopUp").modal('hide');
        self.ResetBook();
    }

    getBooks();
    function getBooks() {
        Riddha.ajax.get(url + "/GetBooks?skipCount=" + ko.toJS(self.skipCount())).done(function (result) {
            var data = Riddha.ko.global.arrayMap(ko.toJS(result.data), BookModel);
            self.Books(data)
        });
    }
    self.AddUpdateBook = function () {
        if (self.Book().title() == "" || self.Book().title() == null || self.Book().title() == undefined) {
            return Riddha.UI.Toast("Plese enter title ! ....", 3);
        }
        if (self.Book().author() == "" || self.Book().author() == null || self.Book().author() == undefined) {
            return Riddha.UI.Toast("Plese enter author ! ....", 3);
        }
        if (self.Book().genre() == "" || self.Book().genre() == null || self.Book().genre() == undefined) {
            return Riddha.UI.Toast("Plese enter genre ! ....", 3);
        }
        if (self.Book().category() == "" || self.Book().category() == null || self.Book().category() == undefined) {
            return Riddha.UI.Toast("Plese enter category ! ....", 3);
        }
        if (self.Book().quantity() == "" || self.Book().quantity() == null || self.Book().quantity() == undefined) {
            return Riddha.UI.Toast("Plese enter quantity ! ....", 3);
        } 
        if (self.Book().price() == "" || self.Book().price() == null || self.Book().price() == undefined) {
            return Riddha.UI.Toast("Plese enter price ! ....", 3);
        }
        if (self.Book().createdDate() == "" || self.Book().createdDate() == null || self.Book().createdDate() == undefined) {
            return Riddha.UI.Toast("Plese choose date ! ....", 3);
        }

        if (self.ModeOfButton() == "Add") {
            Riddha.ajax.post(url + "/AddUpdateBook", ko.toJS(self.Book())).done(function (result) {
                if (result.status == 1) {
                    Riddha.UI.SuccessPopUp(result.message, "success", 0, RedirectToStore);
                }
                else {
                    Riddha.UI.Alert(result.message, "warning", 1);
                }
                getBooks();
            })
        }
        else if (self.ModeOfButton() == "Update") {
            Riddha.ajax.post(url + "/AddUpdateBook", ko.toJS(self.Book())).done(function (result) {
                if (result.status == 1) {
                    self.CloseBookModal();
                    Riddha.UI.SuccessPopUp(result.message, "Success", 0);
                }
                else {
                    Riddha.UI.Alert(result.message, "Warning", 1);
                }
                getBooks();
            })
        }
    }
    self.EditBook = function (model) {
        self.Book(new BookModel(ko.toJS(model)));
        self.OpenBookModal();
        self.ModeOfButton("Update");
    }
    self.DeleteBook = function (model) {
        Riddha.UI.Confirm("Are you sure want to delete ?", function () {
            Riddha.ajax.get(url + "/DeleteBook?bookId=" + model.id(), null)
                .done(function (result) {
                    if (result.status == 1) {
                        Riddha.UI.SuccessPopUp(result.message, "success", 0);
                    }
                    else {
                        Riddha.UI.Alert(result.message, "warning", 1);
                    }
                    getBooks();
                });
        });
    }
    self.Next = function () {
        self.skipCount(self.skipCount() + 10);
        getBooks();
    }
    self.Previous = function () {
        self.skipCount(self.skipCount() - 10);
        getBooks();
    }
    function RedirectToStore() {
        window.location.href = "/StoreManagement/Book/Index";
    }

    self.FilterByCategory = function () {
        Riddha.ajax.post(url + "/FilterBooks", ko.toJS(self.Filter()))
            .done(function (result) {
                var data = Riddha.ko.global.arrayMap(ko.toJS(result.data), BookModel);
                self.Books(data)
            });
    }

    self.SelectedBook = ko.observable();
    self.ResetBook = function () {
        self.Book(new BookModel());
    }
}

