const CryptoJs = require("./crypto-js.js");
const Constants = require("../configs/constants");

function Post(api, data) {
  let token = wx.getStorageSync('token') == null || wx.getStorageSync('token') == '' ? '' : wx.getStorageSync('token');
  let sign = '';
  if (token !== '') {
    sign = Sign(token);
  }
  data.token = token;
  data.sign = sign;
  return new Promise((resolve, reject) => {
    wx.request({
      url: Constants.baseUrl + api,
      method: 'Post',
      data: data,
      header: {
        'Content-Type': 'application/json'
      },
      success(request) {
        resolve(request.data)
      },
      fail(error) {
        reject(error)
      },
      complete(complete) {
        // 加载完成
      }
    })
  });
}
//sign
function Sign(token) {
  let params = [];
  params.push(token);
  let key = Constants.serviceKey; //和服务端对应key
  params.push(key);
  params = params.sort();
  let utf8Params = CryptoJs.enc.Utf8.parse(params.join('').toUpperCase());
  let sign = CryptoJs.MD5(utf8Params).toString(CryptoJs.enc.Hex).substring(5, 29);
  return sign;
}
module.exports = {
  Post: Post
}