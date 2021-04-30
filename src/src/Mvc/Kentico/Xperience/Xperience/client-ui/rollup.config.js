import autoprefixer from 'autoprefixer'
import babel from '@rollup/plugin-babel'
import commonjs from '@rollup/plugin-commonjs'
import clean from 'rollup-plugin-cleaner'
import postcss from 'rollup-plugin-postcss'
import resolve from '@rollup/plugin-node-resolve'
import { terser } from 'rollup-plugin-terser'

const production = !process.env.ROLLUP_WATCH

const configureBundle = (
  name,
  { dest, src } = {
    dest: '../wwwroot/dist',
    src: `${name}.js`
  }
) => {
  return {
    input: `src/${src}`,
    output: {
      file: `${dest}/${name}.min.js`,
      format: 'iife',
      plugins: [terser()],
      sourcemap: !production
    },
    plugins: [
      clean({ targets: [`${dest}/${name}.min.js`, `${dest}/${name}.min.css`] }),
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
      })
    ],
    watch: { exclude: 'node_modules/**' }
  }
}

const configureInlineEditor = (
  identifier,
  options = { src: `editors/${identifier}.js` }
) =>
  configureBundle(identifier, {
    dest: `../wwwroot/dist/PageBuilder/Admin/InlineEditors/${identifier}/`,
    ...options
  })

const configureWidget = (
  identifier,
  options = { src: `widgets/${identifier}.js` }
) =>
  configureBundle(identifier, {
    dest: `../wwwroot/dist/PageBuilder/Public/Widgets/${identifier}/`,
    ...options
  })

export default [
  configureInlineEditor('text-editor'),
  configureWidget('image-widget')
]
