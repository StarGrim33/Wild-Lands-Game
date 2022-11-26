mergeInto(LibraryManager.library, {

  Hello: function () {
    window.alert("Hello, world!");
    console.Log("Hello, world!");
  },

  GiveMePlayerData: function () {
    myGameInstance.SendMessage('Yandex', 'SetName', player.getName());
    myGameInstance.SendMessage('Yandex', 'SetPhoto', player.getPhoto("medium"));
  },

RateGame: function () {
  
    ysdk.feedback.canReview()
        .then(({ value, reason }) => {
            if (value) {
                ysdk.feedback.requestReview()
                    .then(({ feedbackSent }) => {
                        console.Log(feedbackSent);
                    })
            } else {
                console.Log(reason)
            }
          })
    },

  SaveExtern: function (date){
    var dateString = UTF8ToString(date);
    var myobj = JSON.parse(dateString);
    player.setData(myobj);
  },

  ShowAdv : function() {
      ysdk.adv.showFullscreenAdv({
        callbacks: {
        onClose: function(wasShown) {
            console.log("________closed_________")
          // some action after close
        },
        onError: function(error) {
          // some action on error
        }
    }
})
  },

  LoadExtern: function (){
    player.getData().then(_date => {
      const myJSON = JSON.stringify(_date);
      myGameInstance.SendMessage('Progress', 'SetPlayerInfo', myJSON);
    });
  },

});