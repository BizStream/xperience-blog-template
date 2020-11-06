import autoprefixer from 'autoprefixer'
import babel from '@rollup/plugin-babel'
import commonjs from '@rollup/plugin-commonjs'
import clean from 'rollup-plugin-cleaner'
import fs from 'fs'
import postcss from 'rollup-plugin-postcss'
import resolve from '@rollup/plugin-node-resolve'
import serve from 'rollup-plugin-serve'
import { terser } from 'rollup-plugin-terser'

const production = !process.env.ROLLUP_WATCH

const server = !production
  ? serve({
      contentBase: 'dist',
      headers: {
        'Access-Control-Allow-Origin': '*',
        'Access-Control-Allow-Methods': 'GET, POST, PUT, DELETE, PATCH, OPTIONS'
      },
      https: {
        pfx: fs.readFileSync('../App_Data/local-dev.pfx'),
        passphrase: 'changeit' // don't actually change it, this is default value
      },
      open: false,
      verbose: true
    })
  : null

const configureBundle = (name, entryFile) => {
  if (!entryFile) entryFile = name + '.js'
  return {
    input: `src/${entryFile}`,
    output: {
      file: `dist/${name}.min.js`,
      format: 'cjs',
      plugins: [terser()],
      sourcemap: !production
    },
    plugins: [
      clean({ targets: [`dist/${name}.min.js`, `dist/${name}.min.css`] }),
      resolve(),
      commonjs(),
      babel({
        babelrc: false,
        babelHelpers: 'runtime',
        exclude: [/\/core-js\//],
        plugins: ['@babel/plugin-transform-runtime'],
        presets: [['@babel/preset-env', { corejs: 3, useBuiltIns: 'usage' }]]
      }),
      postcss({
        extract: true,
        inject: false,
        minimize: true,
        plugins: [autoprefixer]
      }),
      production && server
    ],
    watch: { exclude: 'node_modules/**' }
  }
}

export default [
  configureBundle('master', 'index.js'),
  configureBundle('home'),
  configureBundle('article')
]
