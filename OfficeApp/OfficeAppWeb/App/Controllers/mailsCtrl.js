(function (app) {
    app.controller('mailsCtrl', ['$scope', 'o365ApiSvc', 'notificationSvc', '$officeSvrvice', function ($scope, o365ApiSvc, notificationSvc, $officeSvrvice) {
        $scope.createDraft = function () {
            o365ApiSvc.createDraft({
                "Subject": "Did you see last night's game?",
                "Importance": "Low",
                "Body": {
                    "ContentType": "HTML",
                    "Content": "They were <b>awesome</b>!"
                },
                "ToRecipients": [
                  {
                      "EmailAddress": {
                          "Address": "jec@officedevgroup.onmicrosoft.com"
                      }
                  }
                ]
            });
        };

        $scope.sendMail = function () {
            o365ApiSvc.sendMail({
                "Message": {
                    "Subject": "Ready for the new Office Development?",
                    "Body": {
                        "ContentType": "HTML",
                        "Content": "Welcome to use <b>Office 365 API</b>!"
                    },
                    "ToRecipients": [
                      {
                          "EmailAddress": {
                              "Address": "jec@officedevgroup.onmicrosoft.com"
                          }
                      }
                    ],
                    "Attachments": []
                },
                "SaveToSentItems": "false"
            });
        };
    }]);
})(app);