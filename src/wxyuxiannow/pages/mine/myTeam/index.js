const Api = require('../../../utils/httpPost');

Page({

  /**
   * 页面的初始数据
   */
  data: {
    dataList: [],
    loadingHiden: true,
    curPage: 1
  },
  /**
   * 生命周期函数--监听页面显示
   */
  onShow: function () {
    let that = this;
    Api.Post('/api/GetMyteam', {}).then(res => {
      if (res.code == 200) {
        that.setData({
          dataList: res.data
        })
      }
    })
  },
  onReachBottom: function () {
    this.setData({
      curPage: this.data.curPage + 1
    });
    this.getMyteam(true)
  },
  onPullDownRefresh: function () {
    this.setData({
      curPage: 1
    });
    this.getMyteam(false)
    wx.stopPullDownRefresh()
  },
  getMyteam: function (append) {
    var that = this;
    wx.showLoading({
      "mask": true
    })
    Api.Post('/api/GetMyteam', {
      pageIndex: this.data.curPage
    }).then(function (res) {
      wx.hideLoading()
      if (res.data.length == 0) {
        let newData = {
          loadingHiden: false
        }
        if (!append) {
          newData.dataList = []
        }
        that.setData(newData);
        return
      }
      let tempArry = [];
      if (append) {
        tempArry = that.data.dataList
      }
      res.data.map((v) => {
        tempArry.push(v)
      });
      that.setData({
        loadingHiden: true,
        dataList: tempArry,
      });
    })
  }
})