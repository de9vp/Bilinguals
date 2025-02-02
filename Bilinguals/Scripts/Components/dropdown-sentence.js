﻿
// save and remove sentence
var sentenceId;
var handleSaveRemove = function () {
    var _savingUrl = '/Sentences/SaveToMyFeaturedSentences?sentenceId=';
    var _removingUrl = '/Sentences/RemoveFromMyFeaturedSentence?userSentenceId=';
    var groupId;
    var userSentenceId;

    // https://stackoverflow.com/questions/29388002/jquery-ajax-sending-multiple-request-on-1-click

    $('.btn-select').on('click', function (e) {
        e.preventDefault();
        var _this = $(this);
        sentenceId = _this.data('sentenceid'); //note: lower case
        userSentenceId = _this.attr('data-usersentenceid');

        //save
        $('.save').off('click').on('click', function (e) {
            e.preventDefault();
            groupId = $(this).data('groupid');
            var url = _savingUrl + sentenceId + '&groupId=' + groupId;

            $.ajax({
                url: url,
                success: function (data) {
                    _this.parent('.btnsave').find('.gn').remove(); 
                    _this.removeClass('btn-outline-custom2').addClass('btn-custom2')
                        .attr('data-usersentenceid', data.userSentenceId)
                        .find('.bi').removeClass('bi-info-lg').addClass('bi-check-lg');
                    _this.parent('.btnsave').prepend(`<div class="gn my-auto rounded-start bg-custom3 my-1"> <span class="px-2"> ${data.groupName} </span> </div>`);
                }
            });

        });

        //remove
        $('.remove').off('click').on('click', function (e) {
            e.preventDefault();
            var URl = _removingUrl + userSentenceId;

            $.ajax({
                url: URl,
                success: function () {
                    _this.parent('.btnsave').find('.gn').remove();
                    _this.removeClass('btn-custom2').addClass('btn-outline-custom2')
                        .attr('data-usersentenceid', "")
                        .find('.bi').removeClass('bi-check-lg').addClass('bi-info-lg');
                },
                error: function () {
                    //thong bao chua luu 
                    console.log("chua luu remove cai quan a");
                },

            });
        });
    });
};

// 
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

// get list groups 
var groupService = (function () {

    var _resourceUrl = '/groups/getusergroups';
    var getUserGroups = () => {
        return simplePromiseRequest(_resourceUrl);
    };

    return { getUserGroups, }
})();

//json request group list
var getUserGroups = function () {
    var _group = [];
    groupService.getUserGroups().then((data) => {
        _group = data;
        var rows = _group.reduce((all, group) => {
            var groupRow = `<li class="d-flex"><a data-groupid="${group.id}" class="dropdown-item mx-2 rounded-pill1 btn-outline-custom2 d-flex justify-content-center save" href="#">${group.name}</a></li>`;
            return all += groupRow;
        }, '');
        $('.save').remove();
        $('.savedropdown').append(rows);
    });
}

// handle create group (promise calback)
var bsModal = (function () {
    var _completeCallback, _beforeShowCallback;
    
    var getGroup = () => { return simplePromiseRequest('/groups/create'); }

    function handleBsModal() {

        

        $('#bsModal').on('show.bs.modal', function () {
            _beforeShowCallback();
        });

        $('#bsModal').on('shown.bs.modal', function (event) {
            // If necessary, you could initiate an AJAX request here (and then do the updating in a callback).
            // Update the modal's content. We'll use jQuery here, but you could use a data binding library or other methods instead.
            var modal = $(this);
            
            getGroup()
                .then((data) => {
                    modal.find('.modal-content').empty().append(data); //data = partial view
                })
                .then(bindAjaxForm) //ajaxForm request submit here
                .then((data) => {
                    _completeCallback(data);
                    // after successful create, save this sentence to new group
                    saveSentence(data.id);
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

    function saveSentence(groupId) {
        var _this = $("div").find(`[data-sentenceid="${sentenceId}"]`); ///https://stackoverflow.com/questions/4191386/jquery-how-to-find-an-element-based-on-a-data-attribute-value
        var url = '/Sentences/SaveToMyFeaturedSentences?sentenceId=' + sentenceId + '&groupId=' + groupId;
        $.ajax({
            url: url,
            success: function (data) {
                _this.parent('.btnsave').find('.gn').remove();
                _this.removeClass('btn-outline-custom2').addClass('btn-custom2')
                    .attr('data-usersentenceid', data.userSentenceId)
                    .find('.bi').removeClass('bi-info-lg').addClass('bi-check-lg');
                _this.parent('.btnsave').prepend(`<div class="gn my-auto rounded-start bg-custom3 my-1"> <span class="px-2"> ${data.groupName} </span> </div>`);
            },
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