var vroot = 'http://try.buildwinjs.com';

angular.module('sample', ['winjs']).controller("sampleController", function ($scope) {

    $scope.items = [
        { title: "Marvelous Mint", text: "Gelato", picture: vroot + "/images/fruits/60Mint.png" },
        { title: "Succulent Strawberry", text: "Sorbet", picture: vroot + "/images/fruits/60Strawberry.png" },
        { title: "Banana Blast", text: "Low-fat frozen yogurt", picture: vroot + "/images/fruits/60Banana.png" },
        { title: "Lavish Lemon Ice", text: "Sorbet", picture: vroot + "/images/fruits/60Lemon.png" },
        { title: "Creamy Orange", text: "Sorbet", picture: vroot + "/images/fruits/60Orange.png" },
        { title: "Very Vanilla", text: "Ice Cream", picture: vroot + "/images/fruits/60Vanilla.png" },
        { title: "Banana Blast", text: "Low-fat frozen yogurt", picture: vroot + "/images/fruits/60Banana.png" },
        { title: "Lavish Lemon Ice", text: "Sorbet", picture: vroot + "/images/fruits/60Lemon.png" }
    ];
});