const App = getApp();
const Api = require('../../utils/httpPost');
const constant = require('../../configs/constants');
Page({

  /**
   * 页面的初始数据
   */
  data: {
    trips: [],
    curPage: 1,
    loadingHiden: true,
    windowWidth: App.systemInfo.windowWidth,
    windowHeight: App.systemInfo.windowHeight,
  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    Api.Post('/api/Scenics', {}).then(res => {
      let tempArry = [];
      if (res.code == 200) {
        res.data.map((v) => {
          let tempObj = v;
          tempObj.pic = `${constant.baseUrl}${v.pic}`;
          tempObj.user.avatar = `${constant.baseUrl}/${v.user.avatar}`
          tempArry.push(tempObj);
        });
        this.setData({
          trips: tempArry
        })
      }
    })
  },
  viewTrip(e) {
    const ds = e.currentTarget.dataset;
    wx.navigateTo({
      url: `./trip/trip?id=${ds.id}`,
    });
  },
  /**
   * 用户点击右上角分享
   */
  onShareAppMessage: function () {

  },
  loadMore: function () {
    this.setData({
      curPage: this.data.curPage + 1
    });
    this.getScenicsList(true)
  },
  onReachBottom: function () {
    this.setData({
      curPage: this.data.curPage + 1
    });
    this.getScenicsList(true)
  },
  onPullDownRefresh: function () {
    this.setData({
      curPage: 1
    });
    this.getScenicsList()
    // wx.stopPullDownRefresh()
  },
  getScenicsList: function (append) {
    var that = this;
    wx.showLoading({
      "mask": true
    })
    Api.Post('/api/Scenics', {
      pageIndex: this.data.curPage
    }).then(function (res) {
      wx.hideLoading()
      if (res.data.length == 0) {
        let newData = {
          loadingHiden: false
        }
        if (!append) {
          newData.goods = []
        }
        that.setData(newData);
        return
      }
      let tempArry = [];
      if (append) {
        tempArry = that.data.trips
      }
      res.data.map((v) => {
        let tempObj = v;
        tempObj.pic = `${constant.baseUrl}${v.pic}`;
        tempObj.user.avatar = `${constant.baseUrl}/${v.user.avatar}`
        tempArry.push(tempObj);
      });
      that.setData({
        loadingHiden: true,
        trips: tempArry,
      });
    })
  }
})