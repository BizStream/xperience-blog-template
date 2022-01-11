import autoprefixer from 'autoprefixer'
import babel from '@rollup/plugin-babel'
import commonjs from '@rollup/plugin-commonjs'
import clean from 'rollup-plugin-cleaner'
import postcss from 'rollup-plugin-postcss'
import postcssNormalize from 'postcss-normalize'
import resolve from '@rollup/plugin-node-resolve'
import { terser } from 'rollup-plugin-terser'

const production = !process.env.ROLLUP_WATCH && process.env.MODE == 'release'
const dest = '../wwwroot'

const configureBundle = (name, entryFile) => {
  if (!entryFile) entryFile = name + '.js'
  return {
    input: `src/${entryFile}`,
    output: {
      dir: dest,
      entryFileNames: `${name}.min.js`,
      format: 'cjs',
      plugins: [terser()],
      sourcemap: !production
    },
    plugins: [
      clean({ targets: [`${dest}/${name}.min.js`, `${dest}/${name}.min.css`] }),
      resolve({ browser: true }),
      commonjs(),
      babel({
        babelrc: false,
        babelHelpers: 'runtime',
        exclude: [/\/core-js\//],
        plugins: ['@babel/plugin-transform-runtime'],
        presets: [
          [
            '@babel/preset-env',
            {
              corejs: 3,
              useBuiltIns: 'usage',
              targets: '> 0.25%, not dead, not ie 11, not op_mini all'
            }
          ]
        ]
      }),
      postcss({
        extract: true,
        inject: false,
        minimize: true,
        plugins: [postcssNormalize, autoprefixer],
        sourceMap: !production
      })
    ],
    watch: { exclude: 'node_modules/**' }
  }
}

export default [
  configureBundle('master', 'index.js'),
  configureBundle('home'),
  configureBundle('article')
]
