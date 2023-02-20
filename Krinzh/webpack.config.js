const path = require('path');
var webpack = require("webpack");

module.exports = {
        context: path.resolve(__dirname),
        entry: path.resolve(__dirname, 'Frontend') + '/index.tsx',
        output: {
                filename: 'bundle.js',  // output bundle file name
                path: path.resolve(__dirname, 'Public'),
                publicPath: '/',
        },
        module: {
                rules: [
                        {
                                test: /\.module\.s(a|c)ss$/,
                                use: ['style-loader', 'css-loader', 'sass-loader'],
                        },
                        {
                                test: /\.tsx?$/,
                                use: [{
                                        loader: 'ts-loader',
                                        options: {
                                                configFile: "tsconfig.json",
                                                projectReferences: true
                                        }
                                }],
                                exclude: /\.d\.ts$/ 
                        },
                        {
                                test: /\.html$/i,
                                loader: "html-loader"
                        },
                        {
                                test: /\.(png|jpg)/,
                                type: 'asset/resource',
                                generator:{
                                        filename: './images/[name][ext]'
                                }
                        },                    
                        {
                                test: /\.d\.ts$/,
                                loader: "dts-loader"
                        },
                ],
        },
        resolve: {
                extensions: ['.ts', '.tsx', '.js'],
        },
}