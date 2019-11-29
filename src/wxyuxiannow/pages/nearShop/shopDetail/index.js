// pages/nearShop/shopDetail/index.js
const Api = require('../../../utils/httpPost');
const constants = require('../.././../configs/constants');
Page({

  /**
   * 页面的初始数据
   */
  data: {
    shopDetail: [],
    shopInfo: {}
  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    let _shopInfo = JSON.parse(options.shopInfo);
    this.setData({
      shopInfo: _shopInfo
    })
  },
  openShopMap: function (e) {
    wx.getLocation({
      type: "gcj02",
      success(res) {
        const latitude = res.latitude
        const longitude = res.longitude
        wx.openLocation({
          latitude,
          longitude,
          scale: 18
        })
      }
    })
  },
  callShopPhone: function (e) {
    let phoneNumber = this.data.shopInfo.phoneNum;
    wx.showModal({
      title: '拨打电话提示',
      content: `你将要给:${phoneNumber}拨打电话`,
      success(res) {
        if (res.confirm) {
          wx.makePhoneCall({
            phoneNumber: phoneNumber
          })
        }
      }
    })
  },
  previewImg: function (e) {
    console.log(e.currentTarget.dataset.index);
    var index = e.currentTarget.dataset.index;
    var imgArr = [];
    this.data.shopDetail.map((v, i) => {
      imgArr.push(v.pic)
    })
    wx.previewImage({
      current: imgArr[index],     //当前图片地址
      urls: imgArr,               //所有要预览的图片的地址集合 数组形式
      success: function (res) { },
      fail: function (res) { },
      complete: function (res) { },
    })
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
    Api.Post('/api/ShopDetails', { shopId: this.data.shopInfo.id }).then(res => {
      let temArr = [];
      res.data.map((v) => {
        let obj = v;
        obj.pic = `${constants.baseUrl}${v.pic}`;
        temArr.push(obj)
      })
      this.setData({
        shopDetail: temArr
      })
    })

  },

  /**
   * 用户点击右上角分享
   */
  onShareAppMessage: function (options) {
    return {
      title: constants.shareProfile,
      path: '/pages/message/index?inviter_id=' + wx.getStorageSync('uid'),
      success: function (res) {
        // 转发成功
      },
      fail: function (res) {
        // 转发失败
      }
    }
  }
})