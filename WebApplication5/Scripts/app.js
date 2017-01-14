// knockout app.js
var todoItemsUri = '/api/todoitems/';
var ownersUri = '/api/owners/';

var viewModel = function () {
    // declaration
    var self = this;
    self.todoItems = ko.observableArray();
    self.error = ko.observable();
    self.detail = ko.observable();
    self.owners = ko.observableArray();

    // a new todo item object
    self.newTodoItem = {
        Owner: ko.observable(),
        Name: ko.observable(),
        CreatedDateTime: ko.observable(),
        IsCompleted: ko.observable()
    }

    // get the todoitem object from DB
    self.getTodoItemDetail = function (item) {
        ajaxHelper(todoItemsUri + item.Id, 'GET').done(function (data) {
            self.detail(data);
        });
    }

    // delete todoitem object from DB
    self.deleteTodoItem = function (item) {
        ajaxHelper(todoItemsUri + item.Id, 'DELETE').done(function (data) {
            self.todoItems.pop(data);
        });
    }

    // add todo item
    self.addTodoItem = function (formElement) {
        var todoitem = {
            OwnerId: self.newTodoItem.Owner().Id,
            Name: self.newTodoItem.Name(),
            CreatedDateTime: self.newTodoItem.CreatedDateTime(),
            IsCompleted: self.newTodoItem.IsCompleted()
        };

        ajaxHelper(todoItemsUri, 'POST', todoitem).done(function (item) {
            self.todoItems.push(item);
        })
    }

    // helper function
    function ajaxHelper(uri, method, data) {
        self.error(''); // clear error message

        return $.ajax({
            type: method,
            url: uri,
            dataType: 'json',
            contentType: 'application/json',
            data: data ? JSON.stringify(data) : null
        }).fail(function (jqXHR, textStatus, errorThrown) {
            self.error(errorThrown);
        });
    }

    // fetch all todo items object from DB
    function getAllTodoItems() {
        ajaxHelper(todoItemsUri, 'GET').done(function (data) {
            self.todoItems(data);
        });
    }

    // fetch all owners object from DB
    function getOwners() {
        ajaxHelper(ownersUri, 'GET').done(function (data) {
            self.owners(data);
        });
    }

    // fetch the initial data. todoItems is accessed in the UI and looped through
    getOwners();
    getAllTodoItems();
};

ko.applyBindings(new viewModel());