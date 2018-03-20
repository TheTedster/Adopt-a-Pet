const path = require('path');
const webpack = require('webpack');
const ExtractTextPlugin = require("extract-text-webpack-plugin");

const srcDir = path.resolve(__dirname, "src");

const extractScss = new ExtractTextPlugin({
    filename: 'style.css',
    allChunks: true,
});

module.exports = {
  entry: './src/index.js',
  output: {
    path: path.resolve(__dirname, 'dist'),
    filename: 'bundle.js'
  },
  module: {
        rules: [{
            test: /\.scss$/,
            use: extractScss.extract({
            fallback: 'style-loader',
            use: ['css-loader', 'sass-loader']
          })
        }]
    },
    plugins: [
    extractScss
  ],
  devServer: {
		historyApiFallback: true,
		contentBase: path.join(__dirname, "dist"),
		publicPath: "/",
		port: 3000,
		open: true
	}
};
