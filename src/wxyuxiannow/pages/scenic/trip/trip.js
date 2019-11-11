// pages/scenic/trip/trip.js
const WxParse = require('../../../wxParse/wxParse.js');
const constants = require('../../../configs/constants.js');
Page({

  /**
   * 页面的初始数据
   */
  data: {
    notice: { title: "go fun" },
    trip: {
      "id": 1,
      "name": "空中草原",
      "cover_image_w640": "https://cn.bing.com/th?id=OIP.05aGhTirRSES_iUYXPOIEwHaEo&pid=Api&rs=1",
      "date_added": "2019-10-28",
      "day_count": 10,
      "view_count": 10000,
      "popular_place_str": "来蔚县 带你领略大美 空中草原",
      "user": {
        "name": "鸟窝",
        "avatar_l": "http://bpic.588ku.com/element_origin_min_pic/16/10/30/528aa13209e86d5d9839890967a6b9c1.jpg"
      }
    }
  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    WxParse.wxParse('trip', 'html', constants.article, this, 5);
  },

  /**
   * 生命周期函数--监听页面初次渲染完成
   */
  onReady: function () {

  },

  /**
   * 生命周期函数--监听页面显示
   */
  onShow: function () {

  },

  /**
   * 生命周期函数--监听页面隐藏
   */
  onHide: function () {

  },

  /**
   * 生命周期函数--监听页面卸载
   */
  onUnload: function () {

  },

  /**
   * 页面相关事件处理函数--监听用户下拉动作
   */
  onPullDownRefresh: function () {

  },

  /**
   * 页面上拉触底事件的处理函数
   */
  onReachBottom: function () {

  },

  /**
   * 用户点击右上角分享
   */
  onShareAppMessage: function () {

  }
})