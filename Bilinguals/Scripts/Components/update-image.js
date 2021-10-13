
var simplePromiseRequest = (url) => {
    return new Promise((resolve, reject) => {
        $.ajax({
            url: url,
            success: (data) => {
                resolve(data);
            },
            error: (res) => {
                reject(res);
            },
        });
    });
}

var getImage = function () {

}

var bsModal = (function () {
    var _completeCallback, _beforeShowCallback;

    var getGroup = () => { return simplePromiseRequest('/images/create'); }

    function handleBsModal() {
        $('#bsModal').on('show.bs.modal', function () {
            _beforeShowCallback();
        });

        $('#bsModal').on('shown.bs.modal', function (event) {
            var modal = $(this);

            getGroup()
                .then((data) => {
                    modal.find('.modal-content').empty().append(data); //data = partial view
                })
                .then(bindAjaxForm) //ajaxForm request submit here
                .then((data) => {
                    _completeCallback(data);
                });
        });
    }

    function bindAjaxForm() {
        var myForm = $('#bsModal').find('form');

        return new Promise((resolve, reject) => {
            myForm.ajaxForm({
                beforeSubmit: function (data) {
                },
                success: function (data) {
                    $('#bsModal').find('button.btn-cl').trigger('click');
                    resolve(data);
                },
                error: reject
            });
        });
    }

    return {
        init: function (beforeShowCallback, completeCallback) {
            handleBsModal();
            _completeCallback = completeCallback;
            _beforeShowCallback = beforeShowCallback;
        }
        ,
        setState: function () { }
    };
})();