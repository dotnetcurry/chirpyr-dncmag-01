/* http://www.knockmeout.net/2011/03/guard-your-model-accept-or-cancel-edits.html */
//wrapper for an observable that protects value until committed
ko.protectedObservable = function (initialValue) {
    //private variables
    var _temp = initialValue;
    var _actual = ko.observable(initialValue);

    var result = ko.dependentObservable({
        read: function () {
            return _actual();
        },
        write: function (newValue) {
            _temp = newValue;
        }
    });

    //commit the temporary value to our observable, if it is different
    result.commit = function () {
        if (_temp !== _actual()) {
            _actual(_temp);
        }
    };

    //notify subscribers to update their value with the original
    result.reset = function () {
        _actual.valueHasMutated();
        _temp = _actual();
    };

    return result;
};

ko.protectedObservableItem = function (item) {
    for (var param in item) {
        if (item.hasOwnProperty(param)) {
            this[param] = ko.protectedObservable(item[param]);
        }
    }

    this.commit = function () {
        for (var property in this) {
            if (this.hasOwnProperty(property) && this[property].commit)
                this[property].commit();
        }
    }
};

ko.toProtectedObservableItemArray = function (sourceArray) {
    var drillItems = ko.utils.arrayMap(sourceArray, function (item) {
        return new ko.protectedObservableItem(item);
    });
    return drillItems;
}