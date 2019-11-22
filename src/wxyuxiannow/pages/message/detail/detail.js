// pages/message/detail/detail.js
const Api = require('../../../utils/httpPost');
const constants = require('../../../configs/constants');
Page({

  /**
   * 页面的初始数据
   */
  data: {
    info: {}
  },
  previewImg: function (e) {
    var index = e.currentTarget.dataset.index;
    var imgArr = e.currentTarget.dataset.picdata;
    wx.previewImage({
      current: imgArr[index],     //当前图片地址
      urls: imgArr,               //所有要预览的图片的地址集合 数组形式
      success: function (res) { },
      fail: function (res) { },
      complete: function (res) { },
    })
  },
  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    Api.Post('/api/GetOneMessage', { id: options.id }).then(res => {
      if (res.code == 200) {

        let tempObj = res.data;
        let tempArry = [];
        var picsArr = JSON.parse(tempObj.pics);
        picsArr.map((v) => {
          let picObj = `${constants.baseUrl}${v}`;
          tempArry.push(picObj)
        });
        tempObj.pics = tempArry;
        this.setData({
          info: tempObj
        })
      }
    })
  },
  /**
   * 用户点击右上角分享
   */
  onShareAppMessage: function () {

  }
})